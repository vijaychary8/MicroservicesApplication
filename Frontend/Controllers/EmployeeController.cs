using Frontend.IService;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Frontend.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) 
        {
        
        _employeeService = employeeService;
        
        }
        public async Task<IActionResult> Index()
        {
            List<EmployeeDetails> empList = new();
            ResponseModel response =await _employeeService.GetAllAsync();
            if(response != null && response.IsSuccess) {

                empList = JsonConvert.DeserializeObject<List<EmployeeDetails>>(Convert.ToString(response.Result));
            }
            return View();
        }
    }
}
