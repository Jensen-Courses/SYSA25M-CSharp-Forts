using eShop.DTOs.Customers;

namespace eShop.Interfaces;

public interface ICustomerRepository
{
    public Task<List<GetAllCustomersDto>> ListAllCustomer();
    public Task<GetCustomerDto> FindCustomer(int id);
    public Task<bool> AddCustomer(PostCustomerDto customer);
    public Task<bool> UpdateCustomer(int id, PutCustomerDto customer);
    public Task<bool> DeleteCustomer(int id);
}
