namespace eShop.Interfaces;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    ISupplierRepository SupplierRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    Task<bool> Complete();
}
