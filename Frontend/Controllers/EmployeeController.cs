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
                TempData["success"] = response?.Message;

                empList = JsonConvert.DeserializeObject<List<EmployeeDetails>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(empList);
        }

        public async Task<IActionResult> CreateEmployee()
        {
           return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeDetails employee)
        {
            if (ModelState.IsValid)
            {
                ResponseModel response = await _employeeService.AddAsync(employee);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = response?.Message;

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }

            return View(employee);
        }
        public async Task<IActionResult> Delete(int empId)
        {
            ResponseModel response = await _employeeService.GetAsync(empId);
            if (response != null && response.IsSuccess)
            {
                EmployeeDetails employee = JsonConvert.DeserializeObject<EmployeeDetails>(Convert.ToString(response.Result));

                return View(employee);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeDetails employee)
        {
            ResponseModel response = await _employeeService.DeleteAsync(employee.EmployeeId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = response?.Message;

                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(employee);
        }
    }
}
