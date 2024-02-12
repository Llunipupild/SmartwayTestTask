using SmartwayTestTask.Models.Base;

namespace SmartwayTestTask.Models.Employee
{
    public class EmployeeModel : IModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public int CompanyId { get; set; }
        public int PassportId { get; set; }
        public int DepartmentId { get; set; }
    }
}
