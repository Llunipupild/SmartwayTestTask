using SmartwayTestTask.Models.Base;
using SmartwayTestTask.Models.Employee;
using SmartwayTestTask.Repositories.Employees.Interface;
using SmartwayTestTask.Services.Employees.Interface;

namespace SmartwayTestTask.Services.Employees
{
    public class EmployeesService : IEmployeesService
    {
        private IEmployeesRepository _employeesRepository;

        public EmployeesService(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task<T> GetOrCreate<T>(T createRequestModel) where T : class, IModel
        {
           return await _employeesRepository.Create(createRequestModel);
        }

        public async Task Update<T>(T model) where T : IModel
        {
            await (_employeesRepository.Update(model));
        }

        public Task<EmployeeModel?> GetEmployeeModelById(int id)
        {
            return _employeesRepository.GetEmployeeModelById(id);
        }

        public Task<EmployeeModel?> GetExistedEmployeeModel(EmployeeModel employeeModel)
        {
            return _employeesRepository.GetOrDefault(employeeModel);
        }

        public async Task<List<EmployeeModel>> GetEmployeeModelsByCompanyId(int id)
        {
            return await _employeesRepository.GetEmployeeModelsByCompanyId(id);
        }

        public async Task<List<EmployeeModel>> GetEmployeeModelsByDepartmentId(int id)
        {
            return await _employeesRepository.GetEmployeeModelsByDepartmentId(id);
        }

        public async Task Delete(int id)
        {
            await _employeesRepository.Delete(id);
        }
    }
}
