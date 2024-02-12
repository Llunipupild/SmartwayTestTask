using SmartwayTestTask.Models.Base;

namespace SmartwayTestTask.Repositories.Base
{
    public interface IRepository
    {
        Task<T> Create<T>(T model) where T : IModel;
        Task<T> GetOrCreate<T>(T model) where T : class, IModel;
        Task Update<T>(T model) where T : IModel;
        Task<T?> GetOrDefault<T>(T model) where T : class, IModel;
        Task Delete(int id);
    }
}
