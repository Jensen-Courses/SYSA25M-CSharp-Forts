using System.Linq.Expressions;
using core.Interfaces;

namespace core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? predicate) : ISpecification<T>
{
    protected BaseSpecification() : this(null) { }

    public Expression<Func<T, bool>>? Predicate => predicate;

    public Expression<Func<T, object>>? OrderByAscending { get; private set; }

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    protected void UserOrderByAscending(Expression<Func<T, object>> orderByAscExpression)
    {
        OrderByAscending = orderByAscExpression;
    }

    protected void UserOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

}
