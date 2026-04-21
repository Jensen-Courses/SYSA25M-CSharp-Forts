using System.Linq.Expressions;
using core.Interfaces;

namespace core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? predicate) : ISpecification<T>
{
    protected BaseSpecification() : this(null) { }

    public Expression<Func<T, bool>>? Predicate => predicate;

    public Expression<Func<T, object>>? OrderByAscending { get; private set; }

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public int Skip { get; private set; }

    public int Take { get; private set; }

    public bool IsPaginationActivated { get; private set; }

    public List<Expression<Func<T, object>>> Includes { get; } = [];
    // För ThenInclude...
    public List<string> NestedIncludes { get; } = [];

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void AddInclude(string includeString)
    {
        NestedIncludes.Add(includeString);
    }

    protected void UseOrderByAscending(Expression<Func<T, object>> orderByAscExpression)
    {
        OrderByAscending = orderByAscExpression;
    }

    protected void UseOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

    protected void ApplyPagination(int take, int skip)
    {
        Take = take;
        Skip = skip;
        IsPaginationActivated = true;
    }

    public IQueryable<T> ApplyPredicate(IQueryable<T> query)
    {
        if (Predicate is not null)
        {
            query = query.Where(Predicate);
        }
        return query;
    }
}
