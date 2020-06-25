using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuiZone.BusinessLogic.Services.Interfaces;
using QuiZone.DataAccess.Models.DTO;
using System.Threading.Tasks;

namespace QuiZone.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;


        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<TokenDTO>> SignIn([FromBody] SignInDTO credentials)
        {
            var result = await authenticationService.SignInAsync(credentials.Email, credentials.Password);

            return result ?? (ActionResult<TokenDTO>)BadRequest();
        }

        [HttpPost("refresh_token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDTO token)
        {
            var result = await authenticationService.TokenAsync(token);

            return result != null
                ? (IActionResult)Ok(result)
                : Forbid();
        }

    }


}
