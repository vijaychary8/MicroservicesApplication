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
            response.IsSuccess = true;

            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel obj)
        {
            var loginResponse = await _authService.Login(obj);
            if(loginResponse.User == null) {
                response.IsSuccess = false;
                response.Message = "User Credentials are Incorrect";
                return BadRequest(response);
            }
            response.Result= loginResponse;
            return Ok(response);
        }

        [HttpPost]
        [Route("AssignRole")]
        public async Task<IActionResult> AssignRole(RegistrationModel obj)
        {
            var assigneRole = await _authService.AssignRole(obj.Email,obj.Role);
            if (!assigneRole)
            {
                response.IsSuccess = false;
                response.Message = "Error Encountered";
                return BadRequest(response);
            }
            response.IsSuccess = true;

            return Ok(response);
        }
    }
}
