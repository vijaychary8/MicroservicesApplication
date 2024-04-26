using AuthApi.Models;

namespace AuthApi.IServices
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationModel obj);
        Task<LoginResponseModel> Login(LoginModel obj);
        Task<Boolean> AssignRole(string email, string rolename);
    }
}
