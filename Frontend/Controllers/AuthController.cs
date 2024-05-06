using Frontend.IService;
using Frontend.Models;
using Frontend.Utility;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

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
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            ResponseModel response = await _authService.LoginAsync(loginModel);
            if (response.IsSuccess && response != null)
            {
                LoginResponseModel responseModel = JsonConvert.DeserializeObject<LoginResponseModel>(Convert.ToString(response.Result));
                ModelState.AddModelError("CustomError", response.Message);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", response.Message);
                return View(loginModel);

            }

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
            ResponseModel assignRole;
            if (response.IsSuccess && response!=null)
            {
                if (string.IsNullOrEmpty(registrationModel.Role))
                {
                    registrationModel.Role = StaticDetails.RoleAdmin;
                }
                assignRole=await _authService.AssignRoleAsync(registrationModel);
                if(assignRole!=null  && assignRole.IsSuccess) {
                    TempData["success"] = "Registration Sucessfull";
                    return RedirectToAction(nameof(Login));
                }
            }
            TempData["error"] = "Already Registered";
            var roleList = new List<SelectListItem>
            {
                new SelectListItem{Text=StaticDetails.RoleAdmin,Value=StaticDetails.RoleAdmin},
                new SelectListItem{Text=StaticDetails.RoleCustomer,Value=StaticDetails.RoleCustomer},

            };
            ViewBag.roleList = roleList;
            return View(registrationModel);
        }
    }
}
