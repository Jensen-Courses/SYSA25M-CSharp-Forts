namespace core.Interfaces;

public interface IUnitOfWork
{
    Task<bool> Complete();
}
