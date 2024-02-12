using SmartwayTestTask.Models.Employee;
using SmartwayTestTask.Services.Base;

namespace SmartwayTestTask.Services.Employees.Interface
{
    public interface IEmployeesService : IService
    {
        Task<EmployeeModel?> GetEmployeeModelById(int id);
        Task<EmployeeModel?> GetExistedEmployeeModel(EmployeeModel employeeModel);
        Task<List<EmployeeModel>> GetEmployeeModelsByCompanyId(int id);
        Task<List<EmployeeModel>> GetEmployeeModelsByDepartmentId(int id);
    }
}
