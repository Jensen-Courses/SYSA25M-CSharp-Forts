using eShop.Data;
using eShop.DTOs.Customers;
using eShop.Entities;
using eShop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eShop.Repositories;

public class CustomerRepository(EShopContext context) : ICustomerRepository
{
    public async Task<bool> AddCustomer(PostCustomerDto customer)
    {
        try
        {
            Customer c = new()
            {
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };

            await context.Customers.AddAsync(c);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteCustomer(int id)
    {
        try
        {
            Customer customer = await context.Customers.FindAsync(id);
            if (customer is null) return false;

            context.Customers.Remove(customer);

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<GetCustomerDto> FindCustomer(int id)
    {
        var customer = await context.Customers.FindAsync(id);
        if (customer is not null)
        {
            GetCustomerDto dto = new()
            {
                CustomerName = customer.FirstName + " " + customer.LastName,
                Email = customer.Email
            };

            return dto;
        }

        return null;
    }

    public async Task<List<GetAllCustomersDto>> ListAllCustomer()
    {
        var items = await context.Customers.ToListAsync();

        List<GetAllCustomersDto> customers = [.. items
            .Select(c => new GetAllCustomersDto()
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName
            })];

        return customers;
    }

    public async Task<bool> UpdateCustomer(int id, PutCustomerDto customer)
    {
        try
        {
            Customer c = await context.Customers.FindAsync(id);
            if (c is null) return false;

            c.Email = customer.Email;
            c.FirstName = customer.FirstName;
            c.LastName = customer.LastName;

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
