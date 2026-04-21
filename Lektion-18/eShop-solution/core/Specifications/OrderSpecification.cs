using core.Entities.Orders;

namespace core.Specifications;

public class OrderSpecification : BaseSpecification<Order>
{
    public OrderSpecification(string email) : base(c => c.CustomerEmail == email)
    {
        UseOrderByDescending(c => c.OrderDate);
    }
}
