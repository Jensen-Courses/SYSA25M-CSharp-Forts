using eShop.Entities;
using eShop.Interfaces;

namespace eShop.Data;

/*  
    SYFTE!!!
    Att implementera Specification Pattern
    Steg 3. Skapar en klass som har en static metod CreateQuery
    och returnerar en IQuerable typ...
    som även tar in en IQuerable type och vår specification = villkor...
    
*/

public class SpecificationValuator<T> where T : BaseEntity
{
    public static IQueryable<T> CreateQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec.Predicate is not null)
        {
            query = query.Where(spec.Predicate);
        }

        if (spec.OrderByAscending is not null)
        {
            query = query.OrderBy(spec.OrderByAscending);
        }

        if (spec.OrderByDescending is not null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        return query;
    }
}
