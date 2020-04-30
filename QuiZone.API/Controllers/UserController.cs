using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuiZone.BusinessLogic.Services.Interfaces;
using QuiZone.BusinessLogic.Utils.Email;
using QuiZone.Common.GlobalErrorHandling;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace QuiZone.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : BaseController<User, UserDTO>
    {
        private readonly IUserService userService;
        private readonly IRegistrationService registrationService;
        public UserController(IMapper mapper, IUserService userService, IRegistrationService registrationService)
            : base(mapper, userService)
        {
            this.userService = userService;
            this.registrationService = registrationService;  
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await userService.GetAllAsync();

            return result.Any()
                ? Ok(result)
                : (IActionResult)NotFound();
        }

        [HttpPost]
        public override async Task<IActionResult> Create([FromBody] UserDTO obj)
        {
            var result = await registrationService.InsertUserAsync(obj);

            return result  == null
               ? (IActionResult)BadRequest()
                : Created($"api/[controller]/{result.Id}", result);
        }


        [HttpGet]
        [Route("email/{email}")]
        public async Task<ActionResult<UserDTO>> GetByEmail([FromRoute]string email)
        {
            var dto = await registrationService.GetUserByEmailAsync(email);

            if (dto == null)
            {
                throw new HttpException(HttpStatusCode.NotFound,
                   $"Користувач з поштою: {email} не знайдениий.");
            }

            return Ok();
        }

        [HttpPut]
        [Route("confirm/{email}/{hash}")]
        public async Task<IActionResult> ConfirmEmail([FromRoute]string email, [FromRoute]string hash)
        {
            var existedUser = await registrationService.GetUserByEmailAsync(email);

            if (existedUser == null)
            {
                return NotFound();
            }

            if (existedUser.Verification)
            {
                return Conflict(existedUser.Email);
            }

            var isConfirm = await registrationService.ConfirmEmailAsync(existedUser, hash);


            return isConfirm
                ? (IActionResult)Ok(existedUser.Email)
                : BadRequest();

        }

       

    }
}