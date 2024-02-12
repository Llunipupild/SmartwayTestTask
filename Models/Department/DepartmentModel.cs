using SmartwayTestTask.Models.Base;

namespace SmartwayTestTask.Models.Department
{
    public class DepartmentModel : IModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
    }
}
