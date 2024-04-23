using AuthApi.IServices;
using AuthApi.Models;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly DbEmpContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthService(DbEmpContext db,UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager) { 
            _dbContext= db;
            _userManager= userManager;
            _roleManager= roleManager;
        
        }

        public async Task<LoginResponseModel> Login(LoginModel obj)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(user => user.UserName.ToLower() == obj.UserName.ToLower());
            bool isValid=await _userManager.CheckPasswordAsync(user, obj.Password);
            if(user==null || isValid== false)
            {
                return new LoginResponseModel() { User=null,Token=""};
            }
            UserModel userModel = new()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };
            LoginResponseModel response = new LoginResponseModel
            {
                User=userModel,
                Token=""
            };
            return response;
        }

        public async Task<String> Register(RegistrationModel obj)
        {
            ApplicationUser user = new()
            {
                UserName = obj.Email,
                Email = obj.Email,
                NormalizedEmail = obj.Email.ToUpper(),
                Name = obj.Name,
                PhoneNumber = obj.PhoneNumber
            };
            try
            {
                var result = await _userManager.CreateAsync(user,obj.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _dbContext.ApplicationUsers.First(u => u.UserName == obj.Email);
                    UserModel userModel = new()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
         }
    }
}
