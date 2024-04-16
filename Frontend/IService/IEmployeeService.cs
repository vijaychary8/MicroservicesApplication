using Frontend.Models;

namespace Frontend.IService
{
    public interface IEmployeeService
    {
        Task<ResponseModel> GetAsync(int employeeId);
         Task<ResponseModel> GetAllAsync();
        Task<ResponseModel> AddAsync(EmployeeDetails employee);
        Task<ResponseModel> UpdateAsync(EmployeeDetails employee);

    }
}
