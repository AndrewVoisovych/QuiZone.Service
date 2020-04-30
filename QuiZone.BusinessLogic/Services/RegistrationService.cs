using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuiZone.BusinessLogic.Services.Interfaces;
using QuiZone.BusinessLogic.Utils.Email;
using QuiZone.Common.GlobalErrorHandling;
using QuiZone.Common.Helpers;
using QuiZone.Common.LoggerService;
using QuiZone.DataAccess.Models.Const;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.UnitOfWork;
using static System.String;

namespace QuiZone.BusinessLogic.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUnitOfWork database;
        private readonly IMapper mapper;

        public RegistrationService(IUnitOfWork database, IMapper mapper, ILoggerManager logger)
        {
            this.database = database;
            this.mapper = mapper;
        }


        public async Task<UserDTO> InsertUserAsync(UserDTO userDTO)
        {
            // Check email
            string inputEmail = userDTO.Email.Trim();
            var existingEmail = await GetUserByEmailAsync(inputEmail);

            if (existingEmail != null)
            {
                throw new HttpException(HttpStatusCode.Conflict,
                    $"Користувач з поштою: {inputEmail}, вжє існує.");
            }

            // Password generate
            string inputPassword = (IsNullOrEmpty(userDTO.Password)) ?
                                    RandomNumbers.Generate() :
                                    userDTO.Password;


            // Get data
            var insertUser = mapper.Map<UserDTO, User>(userDTO);
            insertUser.Password = SHA256Hash.Compute(inputPassword);

            // User data after update/insert
            User insertedUser = new User();

           insertUser.RoleId = (int)UserRolesEnum.USER;
           insertedUser = await database.UserRepository.InsertAsync(insertUser);

            bool isSaved = await database.SaveAsync();

            if (!isSaved)
            {
                return null;
            }

            //Send email
            await SendEmail(insertedUser, insertUser, inputEmail);


            return mapper.Map<User, UserDTO>(insertedUser);
        }

        public async Task SendEmail(User beforeInsertUserData, User afterInsertUserData, string email){
            //generate hash data for confirm email
            string hashconfirmdata =
                EmailConfirmHelper.GetHash(beforeInsertUserData.Id, afterInsertUserData.Verification, afterInsertUserData.Email);
            string linkconfirm =
                EmailConfirmHelper.GetLink("https://localhost:4200", afterInsertUserData.Email, hashconfirmdata);

            //Send email
            string emailMessage =
                EmailBody.Registration.GetBodyMessage(afterInsertUserData.Name, beforeInsertUserData.Email, linkconfirm);

            EmailSender sender = new EmailSender(emailMessage);

            await sender.SendAsync("Registration in QuiZone",
                         email,
                         $"{afterInsertUserData.Name} {afterInsertUserData.Surname}");

        }
        
       

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var user = await database.UserRepository
                .GetByCondition(x => x.Email == email)
                .FirstOrDefaultAsync();

            return user == null
                ? null
                : mapper.Map<User, UserDTO>(user);
        }

        public async Task<bool> ConfirmEmailAsync(UserDTO userDTO, string hash)
        {
            string hashConfirmData = EmailConfirmHelper.GetHash(userDTO.Id, userDTO.Verification, userDTO.Email);
            bool isHashEqual = EmailConfirmHelper.EqualHash(hash, hashConfirmData);


            if (isHashEqual)
            {
                var existedUser = await database.UserRepository.GetByIdAsync(userDTO.Id);

                existedUser.Verification = true;
                var updatedUser = database.UserRepository.Update(existedUser);
                bool isSave = await database.SaveAsync();

                return isSave;

            }

            return false;
        }

    }
}
