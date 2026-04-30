using core.Entities;

namespace core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecificationParams args) : base(c =>
        (string.IsNullOrEmpty(args.Search) || c.ProductName.ToLower().Contains(args.Search.ToLower())) &&
        (string.IsNullOrWhiteSpace(args.ItemNumber) || (c.ItemNumber == args.ItemNumber)))
    {
        // Includes...
        AddInclude(c => c.Supplier!);
        // Paging...
        ApplyPagination(args.PageSize, args.PageSize * (args.PageNumber - 1));

        switch (args.Sort)
        {
            case "priceAsc":
                UseOrderByAscending(c => c.Price);
                break;
            case "priceDesc":
                UseOrderByDescending(c => c.Price);
                break;
            default:
                UseOrderByAscending(c => c.ProductName);
                break;
        }
    }

    public ProductSpecification(string id) : base(c => c.Id == id)
    {
        AddInclude(c => c.Supplier!);
    }
}
