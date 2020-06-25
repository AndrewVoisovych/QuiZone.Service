using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuiZone.BusinessLogic.Services.Base;
using QuiZone.BusinessLogic.Services.Interfaces;
using QuiZone.Common.LoggerService;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Repository.Interfaces;
using QuiZone.DataAccess.UnitOfWork;
using System.Threading.Tasks;

namespace QuiZone.BusinessLogic.Services
{
    public sealed class UserService : CrudService<UserDTO, User>, IUserService
    {
         private readonly IMapper mapper;
        public UserService(
            IUnitOfWork database, 
            ILoggerManager logger,
            IUserRepository userRepository,
            IMapper mapper)
            : base(database, logger, userRepository, mapper)
        {
            this.mapper = mapper;
        }


         public override async Task<UserDTO> UpdateAsync(int id, UserDTO value)
         {
            var existedTableObject = await database.UserRepository.GetByIdAsync(id);

            var tableObject = mapper.Map<UserDTO, User>(value);

            tableObject.Id = id;
            tableObject.CreateDate = existedTableObject.CreateDate;


            var updatedTableObject = repository.Update(tableObject);
            bool isSaved = await database.SaveAsync();

            return isSaved
                ? mapper.Map<User, UserDTO>(updatedTableObject)
                : null;
        }

    }
}
