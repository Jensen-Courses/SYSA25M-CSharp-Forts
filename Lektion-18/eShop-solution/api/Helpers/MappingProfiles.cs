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
        CreateMap<PostProductDto, Product>().ForMember(d => d.ProductName, m => m.MapFrom(s => s.Name));

        CreateMap<Product, ProductDto>()
            .ForMember(d => d.Name, m => m.MapFrom(s => s.ProductName))
            .ForMember(d => d.SupplierName, m => m.MapFrom(s => s.Supplier!.Name));

        CreateMap<Product, ListProductsDto>()
            .ForMember(d => d.Name, m => m.MapFrom(s => s.ProductName))
            .ForMember(d => d.SupplierName, m => m.MapFrom(s => s.Supplier!.Name));

        CreateMap<PutProductDto, Product>().ForMember(d => d.ProductName, m => m.MapFrom(s => s.Name));

        CreateMap<PostSupplierDto, Supplier>();
        CreateMap<Supplier, GetSupplierDto>();

        CreateMap<PostDeliveryMethodDto, DeliveryMethod>();
    }

}
