using AuthApi.IServices;
using AuthApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        protected ResponseModel response;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            response = new();

        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegistrationModel obj)
        {
            var errorMessage = await _authService.Register(obj);
            if(!string.IsNullOrEmpty(errorMessage))
            {
                response.IsSuccess = false;
                response.Message= errorMessage; 
                return BadRequest(response);

            }
            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }
    }
}
