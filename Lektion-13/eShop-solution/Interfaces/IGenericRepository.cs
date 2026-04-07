using eShop.Entities;

namespace eShop.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> FindByIdAsync(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
