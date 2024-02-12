using SmartwayTestTask.Models.Passport;
using SmartwayTestTask.Services.Base;

namespace SmartwayTestTask.Services.Passport.Interface
{
    public interface IPassportService : IService
    {
        Task<PassportModel?> GetPassportModelById(int id);
    }
}
