using SmartwayTestTask.Models.Base;

namespace SmartwayTestTask.Services.Base
{
    public interface IService
    {
        Task<T> GetOrCreate<T>(T model) where T : class, IModel;
        Task Update<T>(T model) where T : IModel;
        Task Delete(int id);
    }
}
