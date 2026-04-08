using System.Linq.Expressions;
using eShop.Data;
using eShop.Entities;
using eShop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eShop.Repositories;
/*  
    SYFTE!!!
    Att implementera Specification Pattern
    Steg 5. 
    
*/
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

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        // Använd ApplySpecification och skicka vårt villkor
        // Vi får tillbaka en IQuerable typ som vi sedan exekverar
        // med ToListAsync => nu sker anropet till databasen...
        return await ApplySpecification(spec).ToListAsync();
    }

    public void Update(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<T?> FindAsync(ISpecification<T> spec)
    {
        // Använd ApplySpecification och skicka vår villkor
        // Vi får tillbaka en IQuerable typ som vi sedan exekverar
        // med FirstOrDefaultAsync => nu sker anropet till databasen...
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        // Anropar vi vår metod CreateQuery och skickar in vår entity och säger
        // returnera en IQuerable type och vi skickar med vårt villkor(spec)
        return SpecificationValuator<T>.CreateQuery(context.Set<T>().AsQueryable(), spec);
    }

}
