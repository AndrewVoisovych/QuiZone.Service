using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuiZone.BusinessLogic.Services.Interfaces;
using QuiZone.BusinessLogic.Utils.JwtAuth;
using QuiZone.Common.GlobalErrorHandling;
using QuiZone.Common.Helpers;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.UnitOfWork;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace QuiZone.BusinessLogic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork database;
        private readonly IJwtFactory jwtFactory;
        private readonly IMapper mapper;

        public AuthenticationService(IUnitOfWork database, IJwtFactory jwtFactory, IMapper mapper)
        {
            this.database = database;
            this.jwtFactory = jwtFactory;
            this.mapper = mapper;
        }


        public async Task<TokenDTO> SignInAsync(string email, string password)
        {
            var user = await database.UserRepository
                .GetByCondition(u => u.Email == email)
                .SingleOrDefaultAsync();

            if (user == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Користувача не знайдено");
            }

            if (IsPasswordTheSame(user, password) == false)
            {
                return null;
            }

            TokenDTO token = jwtFactory.GenerateToken(user.Id, user.Email, user.Role.Role);

            if (token == null)
            {
                return null;
            }

            var oldToken = await database.TokenRepository
                .GetByCondition(x => x.CreateUserId == user.Id)
                .SingleOrDefaultAsync();

            if (oldToken == null)
            {
                await database.TokenRepository.InsertAsync(
                   new Token
                   {
                       RefreshToken = token.RefreshToken,
                       CreateUserId = user.Id
                   }
                 );
            }
            else
            {
                oldToken.RefreshToken = token.RefreshToken;
                oldToken.ModUserId = user.Id;
                database.TokenRepository.Update(oldToken);
            }


            bool IsSave = await database.SaveAsync();

            return IsSave
                ? token
                : null;

        }


        public async Task<TokenDTO> TokenAsync(TokenDTO token)
        {
            var user = await database.UserRepository.GetByIdAsync(
                int.Parse(
                    jwtFactory.GetPrincipalFromExpiredToken(token.AccessToken).jwt.Subject
                    )
            );


            var newToken = jwtFactory.GenerateToken(user.Id, user.Email, user.Role.ToString());

            var oldToken = await database.TokenRepository
                .GetByCondition(x => x.CreateUserId == user.Id)
                .SingleOrDefaultAsync();

            if (oldToken == null)
            {
                await database.TokenRepository.InsertAsync(
                new Token
                {
                    RefreshToken = newToken.RefreshToken,
                    CreateUserId = user.Id
                });
            }
            else
            {
                oldToken.RefreshToken = newToken.RefreshToken;
                oldToken.ModUserId = user.Id;
                database.TokenRepository.Update(oldToken);
            }


            bool IsSave = await database.SaveAsync();

            return IsSave
                ? newToken
                : null;
        }


        public bool IsPasswordTheSame(User user, string password)
        {
            var hashedPassword = SHA256Hash.Compute(password);

            return hashedPassword
                .Zip(user.Password, (a, b) => a == b)
                .Contains(false) == false;
        }
    }
}
