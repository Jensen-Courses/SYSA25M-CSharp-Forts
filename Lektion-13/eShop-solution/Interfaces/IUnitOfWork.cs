namespace eShop.Interfaces;

public interface IUnitOfWork
{
    ISupplierRepository SupplierRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    Task<bool> Complete();
}
