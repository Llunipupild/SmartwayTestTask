using SmartwayTestTask.Models.Department;
using SmartwayTestTask.Services.Base;

namespace SmartwayTestTask.Services.Department.Interface
{
    public interface IDepartmentService : IService
    {
        Task<DepartmentModel?> GetDepartmentModelByName(string name);
        Task<DepartmentModel?> GetDepartmentModelById(int id);
    }
}
