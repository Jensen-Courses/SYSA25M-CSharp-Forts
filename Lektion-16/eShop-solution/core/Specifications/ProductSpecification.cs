using core.Entities;

namespace core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecificationParams args) : base(c =>
        (string.IsNullOrEmpty(args.Search) || c.ProductName.ToLower().Contains(args.Search.ToLower())) &&
        (string.IsNullOrWhiteSpace(args.ItemNumber) || (c.ItemNumber == args.ItemNumber)) &&
        (!args.Brands.Any() || args.Brands.Contains(c.Brand.ToLower())))
    {
        // Paging...
        ApplyPagination(args.PageSize, args.PageSize * (args.PageNumber - 1));

        switch (args.Sort)
        {
            case "priceAsc":
                UserOrderByAscending(c => c.Price);
                break;
            case "priceDesc":
                UserOrderByDescending(c => c.Price);
                break;
            default:
                UserOrderByAscending(c => c.ProductName);
                break;
        }
    }
}
