using eShop.Data;
using eShop.DTOs.Products;
using eShop.Entities;
using eShop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eShop.Repositories;

public class ProductRepository(EShopContext context) : IProductRepository
{
    public async Task<bool> AddProduct(PostProductDto product)
    {
        try
        {
            Supplier supplier = await context.Suppliers.FirstOrDefaultAsync(
                s => s.SupplierName.ToLower().Trim() == product.SupplierName.ToLower().Trim()) ?? throw new Exception("Kunde inte hitta leverantören!");

            Product item = new()
            {
                ItemNumber = product.ItemNumber,
                ProductName = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Description = product.Description,
                Supplier = supplier
            };

            context.Products.Add(item);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteProduct(int id)
    {
        try
        {
            Product product = await context.Products.FindAsync(id);
            if (product is null) return false;
            context.Products.Remove(product);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<GetProductDto> FindProduct(int id)
    {
        try
        {
            var product = await context.Products
            .Where(c => c.Id == id)
            .Select(p => new GetProductDto
            {
                Id = p.Id,
                Name = p.ProductName,
                ItemNumber = p.ItemNumber,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Description = p.Description
            }).SingleOrDefaultAsync() ?? throw new Exception("Hittade ingen produkt!");

            return product;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<GetProductDto> FindProduct(string itemNumber)
    {
        try
        {
            var product = await context.Products
            .Where(c => c.ItemNumber == itemNumber)
            .Select(p => new GetProductDto
            {
                Id = p.Id,
                Name = p.ProductName,
                ItemNumber = p.ItemNumber,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Description = p.Description
            }).SingleOrDefaultAsync() ?? throw new Exception("Hittade ingen produkt!");

            return product;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<GetProductsDto>> ListAllProducts()
    {
        var products = await context.Products
            .Select(product => new GetProductsDto
            {
                Id = product.Id,
                ItemNumber = product.ItemNumber,
                Name = product.ProductName,
                Price = product.Price
            }).ToListAsync();

        return products;

    }

    public async Task<bool> UpdateProduct(int id, PutProductDto product)
    {
        try
        {
            Product item = await context.Products.FindAsync(id);
            if (item is null) return false;

            item.ItemNumber = product.ItemNumber;
            item.Description = product.Description;
            item.Price = product.Price;
            item.ProductName = product.Name;
            item.ImageUrl = product.ImageUrl;

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }



    }
}
