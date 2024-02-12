using SmartwayTestTask.Models.Department;
using SmartwayTestTask.Models.Passport;

namespace SmartwayTestTask.Models.Employees
{
    public class FullEmloyeeModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public int CompanyId { get; set; }
        public PassportModel Passport { get; set; } = null!;
        public DepartmentModel Department { get; set; } = null!;
    }
}
