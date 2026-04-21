using api.DTOs.Orders;
using api.DTOs.Products;
using api.DTOs.Suppliers;
using AutoMapper;
using core.Entities;

namespace api.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PostProductDto, Product>()
            .ForMember(d => d.ProductName, m => m.MapFrom(s => s.Name));
        CreateMap<Product, GetProductsDto>()
            .ForMember(d => d.Name, m => m.MapFrom(s => s.ProductName));

        CreateMap<PostSupplierDto, Supplier>();
        CreateMap<Supplier, GetSupplierDto>();

        CreateMap<PostDeliveryMethodDto, DeliveryMethod>();
    }

}
