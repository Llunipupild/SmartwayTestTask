using SmartwayTestTask.Models.Base;
using SmartwayTestTask.Models.Passport;
using SmartwayTestTask.Repositories.Passport.Interface;
using SmartwayTestTask.Services.Passport.Interface;

namespace SmartwayTestTask.Services.Passport
{
    public class PassportService : IPassportService
    {
        private IPassportRepository _passportRepository;

        public PassportService(IPassportRepository passportRepository)
        {
            _passportRepository = passportRepository;
        }

        public async Task<T> GetOrCreate<T>(T model) where T : class, IModel
        {
            return await _passportRepository.GetOrCreate(model);
        }

        public async Task Update<T>(T model) where T : IModel
        {
            await _passportRepository.Update(model);
        }

        public async Task<PassportModel?> GetPassportModelById(int id)
        {
            return await _passportRepository.GetPassportModelById(id);
        }

        public async Task Delete(int id)
        {
            await _passportRepository.Delete(id);
        }
    }
}
