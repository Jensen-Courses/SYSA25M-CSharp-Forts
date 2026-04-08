using eShop.Entities;

namespace eShop.Specifications;
/*  
    SYFTE!!!
    Att implementera Specification Pattern
    Steg 6. 
    Här skapar vi villka frågor osv... som vi vill kunna köra med ProductsController...
    Vi ärver ifrån BaseSpecification och skickar med typen som ska användas
    I detta fallet Product...
*/
public class ProductSpecification : BaseSpecification<Product>
{
    // Här anger vi villka eventuella argument(frågor) som vi behöver kunna exekvera i vår controller.
    // Vi behöver kunna hitta en produkt på itemNumber eller produkter på brand...
    public ProductSpecification(string? itemNumber, string? brand, string? sort) : base(
        c => (string.IsNullOrWhiteSpace(itemNumber) || (c.ItemNumber == itemNumber)) &&
        (string.IsNullOrWhiteSpace(brand) || (c.Brand.ToLower() == brand.ToLower())))
    {
        switch (sort)
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
