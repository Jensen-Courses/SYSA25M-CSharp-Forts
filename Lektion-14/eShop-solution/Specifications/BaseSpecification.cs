using System.Linq.Expressions;
using eShop.Interfaces;

namespace eShop.Specifications;

/*  
    SYFTE!!!
    Att implementera Specification Pattern
    Steg 2. Skapa en klass om implementerar vårt interface(ISpecification<T>)...
    Skapar två constructors
        En som tar in ett villkor som argument
        En som inte tar in argument(default constructor)
*/
public class BaseSpecification<T>(Expression<Func<T, bool>>? predicate) : ISpecification<T>
{
    // Default constructor utan argument...
    protected BaseSpecification() : this(null) { }

    // Vi skapar en egenskap Predicate som endast är en getter...
    public Expression<Func<T, bool>>? Predicate => predicate;
    // public Expression<Func<T, bool>>? Predicate { 
    //     get 
    //     { 
    //         return predicate;
    //     } 
    // }

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
