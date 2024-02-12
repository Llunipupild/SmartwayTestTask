using SmartwayTestTask.Models.Passport;
using SmartwayTestTask.Repositories.Base;

namespace SmartwayTestTask.Repositories.Passport.Interface
{
    public interface IPassportRepository : IRepository
    {
        public Task<PassportModel?> GetPassportModelById(int id);
    }
}
