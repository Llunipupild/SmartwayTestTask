using SmartwayTestTask.Models.Employee;
using SmartwayTestTask.Models.Request.Create;

namespace SmartwayTestTask.Tools.CreateRequestExtension
{
    public static class CreateRequestModelExtension
    {
        public static EmployeeModel GetEmployeeModel(this CreateRequestModel createRequestModel)
        {
            return new()
            {
                Name = createRequestModel.Name,
                Surname = createRequestModel.Surname,
                Phone = createRequestModel.Phone,
                CompanyId = createRequestModel.CompanyId,
                PassportId = createRequestModel.Passport.Id,
                DepartmentId = createRequestModel.Department.Id
            };
        }
    }
}
