using System.Linq.Expressions;

namespace core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Predicate { get; }
    Expression<Func<T, object>>? OrderByAscending { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> NestedIncludes { get; }

    int Skip { get; }
    int Take { get; }
    bool IsPaginationActivated { get; }
    IQueryable<T> ApplyPredicate(IQueryable<T> query);
}
