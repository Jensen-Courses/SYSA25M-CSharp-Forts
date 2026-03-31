using eShop.DTOs.Suppliers;
using eShop.Interfaces;

namespace eShop.Mock;

public class MockSupplier : ISupplierRepository
{
    public Task<int> AddSupplier(PostSupplierDto supplier)
    {
        throw new NotImplementedException();
    }

    public Task<GetSupplierDto> FindSupplier(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<GetSuppliersDto>> ListAllSuppliers()
    {
        throw new NotImplementedException();
    }
}
