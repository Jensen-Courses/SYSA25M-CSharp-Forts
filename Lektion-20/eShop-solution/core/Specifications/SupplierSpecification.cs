using core.Entities;

namespace core.Specifications;

public class SupplierSpecification : BaseSpecification<Supplier>
{
    public SupplierSpecification(SupplierSpecificationParams args) : base(c =>
    string.IsNullOrWhiteSpace(args.SupplierName) || c.Name.ToLower().Contains(args.SupplierName.ToLower()))
    {
        ApplyPagination(args.PageSize, args.PageSize * (args.PageNumber - 1));

        switch (args.Sort)
        {
            default:
                UseOrderByAscending(c => c.Name);
                break;
        }
    }
}
