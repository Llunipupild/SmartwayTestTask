using SmartwayTestTask.Models.Department;
using SmartwayTestTask.Repositories.Base;

namespace SmartwayTestTask.Repositories.Department.Interface
{
    public interface IDepartmentRepository : IRepository
    {
        public Task<DepartmentModel?> GetDepartmentModelById(int id);
        public Task<DepartmentModel?> GetDepartmentModelByName(string name);
    }
}
