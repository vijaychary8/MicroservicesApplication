using Frontend.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace Frontend.IService
{
    public interface IAuthService
    {
        Task<ResponseModel> LoginAsync(LoginModel loginModel);
        Task<ResponseModel> RegisterAsync(RegistrationModel registrationModel);
        Task<ResponseModel> AssignRoleAsync(RegistrationModel registrationModel);

    }
}
