using System.Threading.Tasks;
using QuiZone.BusinessLogic.Services.Base;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;

namespace QuiZone.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// User model CRUD
    /// </summary>
    public interface IUserService : ICrudService<UserDTO, User>
    {
        //Task<UserDTO> UpdatePassword(int id, string oldPassword, string newPassword);
    }
}
