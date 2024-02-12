using SmartwayTestTask.Models.Employee;
using SmartwayTestTask.Models.Request.Update;

namespace SmartwayTestTask.Tools.UpdateRequestExtension
{
    public static class UpdateRequestExtension
    {
        public static void SetDefaultValue(this UpdateRequestModel updateRequestModel, EmployeeModel employeeModel)
        {
            if (string.IsNullOrEmpty(updateRequestModel.Name)) {
                updateRequestModel.Name = employeeModel.Name;
            }
            if (string.IsNullOrEmpty(updateRequestModel.Surname)) {
                updateRequestModel.Name = employeeModel.Surname;
            }
            if (string.IsNullOrEmpty(updateRequestModel.Phone)) {
                updateRequestModel.Phone = employeeModel.Phone;
            }
            if (updateRequestModel.CompanyId == null) {
                updateRequestModel.CompanyId = employeeModel.CompanyId;
            }
            if (updateRequestModel.Passport == null) {
                updateRequestModel.Passport = new() { Id = employeeModel.PassportId };
            }
            if (updateRequestModel.Department == null) {
                updateRequestModel.Department = new() { Id = employeeModel.DepartmentId };
            }
        }
    }
}
