using eShop.Data;
using eShop.DTOs.Suppliers;
using eShop.Entities;
using eShop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eShop.Repositories;

public class SupplierRepository(EShopContext context) : ISupplierRepository
{
    public async Task<int> AddSupplier(PostSupplierDto supplier)
    {
        try
        {
            var entity = new Supplier
            {
                SupplierName = supplier.Name,
                Email = supplier.Email,
                Phone = supplier.Phone,
                Address = supplier.Address,
                PostalCode = supplier.PostalCode,
                City = supplier.City
            };

            context.Suppliers.Add(entity);
            await context.SaveChangesAsync();

            return entity.SupplierId;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<GetSupplierDto> FindSupplier(int id)
    {
        try
        {
            Supplier supplier = await context.Suppliers.FindAsync(id) ?? throw new Exception("Kunde inte hitta leverantör");

            GetSupplierDto dto = new()
            {
                Id = supplier.SupplierId,
                Name = supplier.SupplierName,
                Email = supplier.Email,
                Phone = supplier.Phone,
                Address = supplier.Address,
                PostalCode = supplier.PostalCode,
                City = supplier.City
            };

            return dto;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<GetSuppliersDto>> ListAllSuppliers()
    {
        try
        {
            List<Supplier> result = await context.Suppliers.ToListAsync();
            List<GetSuppliersDto> suppliers = [];

            foreach (var supplier in result)
            {
                var dto = new GetSuppliersDto
                {
                    Id = supplier.SupplierId,
                    Name = supplier.SupplierName,
                    Email = supplier.Email,
                    Phone = supplier.Phone
                };

                suppliers.Add(dto);
            }

            return suppliers;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ett fel inträffade i ListAllSuppliers, {ex.Message}");
        }
    }
}
