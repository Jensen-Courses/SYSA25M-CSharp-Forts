using core.Entities;
using core.Interfaces;

namespace infrastructure.Data;

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
