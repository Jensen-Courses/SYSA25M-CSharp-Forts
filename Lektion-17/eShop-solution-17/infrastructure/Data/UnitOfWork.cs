using core.Entities;
using core.Interfaces;

namespace infrastructure.Data;

public class UnitOfWork(EShopContext context) : IUnitOfWork
{
    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        throw new NotImplementedException();
    }
}
