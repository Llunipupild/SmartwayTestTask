using SmartwayTestTask.Models.Base;

namespace SmartwayTestTask.Models.Passport
{
    public class PassportModel : IModel
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Number { get; set; } = null!;
    }
}
