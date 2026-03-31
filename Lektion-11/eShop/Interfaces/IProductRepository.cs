using eShop.DTOs.Products;

namespace eShop.Interfaces;

public interface IProductRepository
{
    public Task<List<GetProductsDto>> ListAllProducts();
    public Task<GetProductDto> FindProduct(int id);
    public Task<int> AddProduct(PostProductDto product);
}
