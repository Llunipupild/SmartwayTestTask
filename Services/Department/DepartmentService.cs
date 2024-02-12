using SmartwayTestTask.Models.Base;
using SmartwayTestTask.Models.Department;
using SmartwayTestTask.Repositories.Department.Interface;
using SmartwayTestTask.Services.Department.Interface;

namespace SmartwayTestTask.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        private IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<T> GetOrCreate<T>(T model) where T : class, IModel
        {
            return await _departmentRepository.GetOrCreate(model);
        }

        public async Task Update<T>(T model) where T : IModel
        {
            await _departmentRepository.Update(model);
        }

        public async Task<DepartmentModel?> GetDepartmentModelById(int id)
        {
            return await _departmentRepository.GetDepartmentModelById(id);
        }

        public async Task<DepartmentModel?> GetDepartmentModelByName(string name)
        {
            return await _departmentRepository.GetDepartmentModelByName(name);
        }

        public async Task Delete(int id)
        {
            await _departmentRepository.Delete(id);
        }
    }
}
