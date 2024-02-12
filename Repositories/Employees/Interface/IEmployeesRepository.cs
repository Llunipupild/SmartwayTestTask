using SmartwayTestTask.Models.Base;
using SmartwayTestTask.Models.Employee;
using SmartwayTestTask.Repositories.Base;

namespace SmartwayTestTask.Repositories.Employees.Interface
{
    public interface IEmployeesRepository : IRepository
    {
        Task<EmployeeModel?> GetEmployeeModelById(int id);
        Task<List<EmployeeModel>> GetEmployeeModelsByCompanyId(int id);
        Task<List<EmployeeModel>> GetEmployeeModelsByDepartmentId(int id);
    }
}
