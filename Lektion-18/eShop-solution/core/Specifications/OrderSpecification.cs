using System.Security.Cryptography.X509Certificates;
using core.Entities.Orders;

namespace core.Specifications;

public class OrderSpecification : BaseSpecification<Order>
{
    public OrderSpecification(string email) : base(c => c.CustomerEmail == email)
    {
        AddInclude(c => c.OrderItems);
        AddInclude(c => c.DeliveryMethod);
        UseOrderByDescending(c => c.OrderDate);
    }

    public OrderSpecification(string email, string id) : base(c => c.CustomerEmail == email && c.Id == id)
    {
        AddInclude(c => c.OrderItems);
        AddInclude(c => c.DeliveryMethod);
        // AddInclude("OrderItems");
        // AddInclude("DeliveryMethod");
    }
}
