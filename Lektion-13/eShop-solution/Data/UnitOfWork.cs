using eShop.Interfaces;
using eShop.Repositories;

namespace eShop.Data;

public class UnitOfWork(EShopContext context) : IUnitOfWork
{
    public ISupplierRepository SupplierRepository => new SupplierRepository(context);

    public ICustomerRepository CustomerRepository => new CustomerRepository(context);

    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
