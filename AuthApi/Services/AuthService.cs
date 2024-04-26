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
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(DbEmpContext db,UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,IJwtTokenGenerator jwtTokenGenerator) { 
            _dbContext= db;
            _userManager= userManager;
            _roleManager= roleManager;
            _jwtTokenGenerator= jwtTokenGenerator;
        
        }

        public async Task<bool> AssignRole(string email, string rolename)
        {
            var user=_dbContext.ApplicationUsers.FirstOrDefault(x=>x.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if(!_roleManager.RoleExistsAsync(rolename).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(rolename)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, rolename);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseModel> Login(LoginModel obj)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(user => user.UserName.ToLower() == obj.UserName.ToLower());
            bool isValid=await _userManager.CheckPasswordAsync(user, obj.Password);
            if(user==null || isValid== false)
            {
                return new LoginResponseModel() { User=null,Token=""};
            }

          var token=  _jwtTokenGenerator.GenerateToken(user);



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
                Token=token
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
