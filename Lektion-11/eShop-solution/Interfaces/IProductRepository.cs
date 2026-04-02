using eShop.DTOs.Products;

namespace eShop.Interfaces;

public interface IProductRepository
{
    public Task<List<GetProductsDto>> ListAllProducts();
    public Task<GetProductDto> FindProduct(int id);
    public Task<GetProductDto> FindProduct(string itemNumber);
    public Task<int> AddProduct(PostProductDto product);
    public Task<bool> UpdateProduct(int id, PutProductDto product);
    public Task DeleteProduct(int id);
}
