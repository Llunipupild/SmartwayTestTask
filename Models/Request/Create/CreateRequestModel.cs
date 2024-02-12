using SmartwayTestTask.Models.Base;
using SmartwayTestTask.Models.Department;
using SmartwayTestTask.Models.Passport;
using System.ComponentModel.DataAnnotations;

namespace SmartwayTestTask.Models.Request.Create
{
    public class CreateRequestModel : IModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Surname { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public PassportModel Passport { get; set; } = null!;

        [Required]
        public DepartmentModel Department { get; set; } = null!;
    }
}
