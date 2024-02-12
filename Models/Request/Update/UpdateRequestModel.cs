using SmartwayTestTask.Models.Base;
using SmartwayTestTask.Models.Department;
using SmartwayTestTask.Models.Passport;
using System.ComponentModel.DataAnnotations;

namespace SmartwayTestTask.Models.Request.Update
{
    public class UpdateRequestModel : IModel
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; } = null!;
        public string? Surname { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public int? CompanyId { get; set; }
        public PassportModel? Passport { get; set; }
        public DepartmentModel? Department { get; set; }
    }
}
