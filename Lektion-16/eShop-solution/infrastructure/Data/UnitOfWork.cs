using core.Interfaces;

namespace infrastructure.Data;

public class UnitOfWork(EShopContext context) : IUnitOfWork
{
    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
