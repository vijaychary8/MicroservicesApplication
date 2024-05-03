using Frontend.IService;
using Frontend.Models;
using Frontend.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Frontend.Controllers
{
    public class AuthController : Controller
    {
        public readonly IAuthService _authService;
        public AuthController(IAuthService authService) { 
        
        _authService = authService;
        
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>
            {
                new SelectListItem{Text=StaticDetails.RoleAdmin,Value=StaticDetails.RoleAdmin},
                new SelectListItem{Text=StaticDetails.RoleCustomer,Value=StaticDetails.RoleCustomer},

            };
            ViewBag.roleList = roleList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {
            ResponseModel response=await _authService.RegisterAsync(registrationModel);
            if (response.IsSuccess && response!=null)
            {
                if (string.IsNullOrEmpty(registrationModel.Role))
                {
                    registrationModel.Role = StaticDetails.RoleAdmin;
                }
            }

            return View();
        }
    }
}
