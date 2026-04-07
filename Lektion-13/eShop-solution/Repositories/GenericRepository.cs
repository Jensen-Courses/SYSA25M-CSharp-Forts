using eShop.Data;
using eShop.Entities;
using eShop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eShop.Repositories;

public class GenericRepository<T>(EShopContext context) : IGenericRepository<T> where T : BaseEntity
{
    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<T> FindByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }
}
