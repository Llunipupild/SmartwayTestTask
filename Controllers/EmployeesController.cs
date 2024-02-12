using Microsoft.AspNetCore.Mvc;
using SmartwayTestTask.Models.Department;
using SmartwayTestTask.Models.Employee;
using SmartwayTestTask.Models.Employees;
using SmartwayTestTask.Models.Request.Create;
using SmartwayTestTask.Models.Request.Update;
using SmartwayTestTask.Services.Department.Interface;
using SmartwayTestTask.Services.Employees.Interface;
using SmartwayTestTask.Services.Passport.Interface;
using SmartwayTestTask.Tools.CreateRequestExtension;
using SmartwayTestTask.Tools.UpdateRequestExtension;

namespace SmartwayTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeesService _employeesService;
        private IDepartmentService _departmentService;
        private IPassportService _passportService;

        public EmployeesController(IEmployeesService employeesService, IDepartmentService departmentService, IPassportService passportService)
        {
            _employeesService = employeesService;
            _departmentService = departmentService;
            _passportService = passportService;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(int))]
        [ProducesResponseType(409, Type = typeof(int))]
        public async Task<IActionResult> AddEmployee(CreateRequestModel createRequestModel)
        {
            createRequestModel.Department = await _departmentService.GetOrCreate(createRequestModel.Department);
            createRequestModel.Passport = await _passportService.GetOrCreate(createRequestModel.Passport);
            EmployeeModel createRequestEmployeeModel = createRequestModel.GetEmployeeModel();
            EmployeeModel? existedModel = await _employeesService.GetExistedEmployeeModel(createRequestEmployeeModel);
            if (existedModel != null) {
                return StatusCode(StatusCodes.Status409Conflict, existedModel.Id);
            }

            EmployeeModel employeeModel = await _employeesService.GetOrCreate(createRequestEmployeeModel);
            return StatusCode(StatusCodes.Status201Created, employeeModel.Id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            EmployeeModel? existedModel = await _employeesService.GetEmployeeModelById(id);
            if (existedModel == null) {
                return StatusCode(StatusCodes.Status409Conflict, "Работника с таким id не существует");
            }

            await _passportService.Delete(existedModel.PassportId);
            await _employeesService.Delete(id);
            return Ok($"Сотрудник с id={id} удалён");
        }

        [HttpGet("companyId={id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetEmployeeByCompanyId(int id)
        {
            List<EmployeeModel> employeeModels = await _employeesService.GetEmployeeModelsByCompanyId(id);
            return Ok(await GetFullEmloyeeModels(employeeModels));
        }

        [HttpGet("departmentName={departmentName}")]
        [ProducesResponseType(200, Type = typeof(List<FullEmloyeeModel>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetEmployeeByDepartmentName(string departmentName)
        {
            DepartmentModel? departmentModel = await _departmentService.GetDepartmentModelByName(departmentName);
            if(departmentModel == null) {
                return StatusCode(StatusCodes.Status404NotFound, $"Отдела с именем {departmentName} не существует");
            }

            List<EmployeeModel> employeeModels = await _employeesService.GetEmployeeModelsByDepartmentId(departmentModel.Id);
            return Ok(await GetFullEmloyeeModels(employeeModels));
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateEmployeeData(UpdateRequestModel updateRequestModel)
        {
            EmployeeModel? existedModel = await _employeesService.GetEmployeeModelById(updateRequestModel.Id);
            if (existedModel == null) {
                return StatusCode(StatusCodes.Status404NotFound, "Работника с таким id не существует");
            }

            updateRequestModel.SetDefaultValue(existedModel);
            updateRequestModel.Department = await _departmentService.GetDepartmentModelById(updateRequestModel.Department!.Id);
            updateRequestModel.Passport = await _passportService.GetPassportModelById(updateRequestModel.Passport!.Id);
            await _employeesService.Update(updateRequestModel);

            return Ok($"Данные сотрудника с id={updateRequestModel.Id} были изменены");
        }

        //надо делать через join, но у меня что-то не работает и дедлайн горит
        private async Task<List<FullEmloyeeModel>> GetFullEmloyeeModels(List<EmployeeModel> employeeModels)
        {
            List<FullEmloyeeModel> result = new();
            foreach (EmployeeModel employeeModel in employeeModels) {
                FullEmloyeeModel fullEmloyeeModel = new() {
                    Id = employeeModel.Id,
                    Name = employeeModel.Name,
                    Surname = employeeModel.Surname,
                    Phone = employeeModel.Phone,
                    CompanyId = employeeModel.CompanyId,
                    Department = await _departmentService.GetDepartmentModelById(employeeModel.DepartmentId),
                    Passport = await _passportService.GetPassportModelById(employeeModel.PassportId)
                };

                result.Add(fullEmloyeeModel);
            }
            return result;
        }
    }
}
