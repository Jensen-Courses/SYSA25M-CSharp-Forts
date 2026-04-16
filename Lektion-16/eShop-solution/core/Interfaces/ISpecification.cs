using System.Linq.Expressions;

namespace core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Predicate { get; }
    Expression<Func<T, object>>? OrderByAscending { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }

    // Paginerings egenskaper...
    int Skip { get; }
    int Take { get; }
    bool IsPaginationActivated { get; }
    IQueryable<T> ApplyPredicate(IQueryable<T> query);
}
