namespace eShop.Interfaces;

public interface IUnitOfWork
{
    Task<bool> Complete();
}
