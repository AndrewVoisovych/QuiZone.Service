using QuiZone.DataAccess.Models.DTO;
using System.Threading.Tasks;

namespace QuiZone.BusinessLogic.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<UserDTO> InsertUserAsync(UserDTO userDTO);
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<bool> ConfirmEmailAsync(UserDTO userDTO, string hash);

    }
}
