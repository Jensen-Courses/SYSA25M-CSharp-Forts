using eShop.Data;
using eShop.Entities;
using eShop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eShop.Repositories;

public class GenericRepository<T>(EShopContext context) : IGenericRepository<T> where T : BaseEntity
{
    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
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
        context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
