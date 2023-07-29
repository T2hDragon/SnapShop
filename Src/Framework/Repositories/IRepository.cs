namespace SnapShop.Framework.Repositories
{
    public interface IRepository<T>
    {
        Task<T?> GetById(Guid id);
        Task<List<T>> GetAll();
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
    }
}
