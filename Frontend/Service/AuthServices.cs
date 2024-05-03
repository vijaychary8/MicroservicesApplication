using Frontend.IService;
using Frontend.Models;
using Frontend.Utility;
using Microsoft.AspNetCore.Identity.Data;

namespace Frontend.Service
{
    public class AuthServices : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthServices(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseModel> AssignRoleAsync(RegistrationModel registrationModel)
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = registrationModel,
                Url = StaticDetails.AuthApiBase + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseModel> LoginAsync(LoginRequest loginRequest)
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = loginRequest,
                Url = StaticDetails.AuthApiBase + "/api/auth/login"
            });
        }

        public async Task<ResponseModel> RegisterAsync(RegistrationModel registrationModel)
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = registrationModel,
                Url = StaticDetails.AuthApiBase + "/api/auth/register"
            });
        }
    }
}
