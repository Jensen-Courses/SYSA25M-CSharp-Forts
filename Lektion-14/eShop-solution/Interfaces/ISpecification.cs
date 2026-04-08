using System.Linq.Expressions;

namespace eShop.Interfaces;

/*  
    SYFTE!!!
    Att implementera Specification Pattern
    Steg 1. Skapa ett interface med de metoder som vi kommer att behöva använda...
*/
public interface ISpecification<T>
{
    // Så kommer denna egenskap att bli vår where hantering(villkor)...
    Expression<Func<T, bool>>? Predicate { get; }
    // Dessa egenskaper kommer att hjälpa oss att sortera stigande eller fallande...
    Expression<Func<T, object>>? OrderByAscending { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
}
