using core.Entities;

namespace core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<T?> FindByIdAsync(string id);
    Task<T?> FindAsync(ISpecification<T> spec);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> CountAsync(ISpecification<T> spec);
    bool CheckIfExists(string id);
}
