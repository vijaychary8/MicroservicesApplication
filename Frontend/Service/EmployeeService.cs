using Frontend.IService;
using Frontend.Models;
using Frontend.Utility;

namespace Frontend.Service
{
    public class EmployeeService:IEmployeeService
    {   
        public readonly IBaseService _baseService;
        public EmployeeService(IBaseService baseService) { 
        _baseService = baseService;
        }

        public async Task<ResponseModel>  AddAsync(EmployeeDetails employee)   
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data=employee,
                Url = StaticDetails.EmployeeAPIBase + "/api/employee/Add"
            });
        }

       public async  Task<ResponseModel> GetAllAsync()
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType=StaticDetails.ApiType.GET,
                Url=StaticDetails.EmployeeAPIBase+ "/api/employee"
            });
        }

        public async  Task<ResponseModel> GetAsync(int employeeId)
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.EmployeeAPIBase + "/api/employee/" + employeeId
            });
        }

        public async Task<ResponseModel> UpdateAsync(EmployeeDetails employee)
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = employee,
                Url = StaticDetails.EmployeeAPIBase + "/api/employee/update"
            });
        }
        public async Task<ResponseModel> DeleteAsync(int employeeId)
        {
            return await _baseService.SendAsync(new RequestModel()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.EmployeeAPIBase + "/api/employee/" + employeeId

            });
        }
    }
}
