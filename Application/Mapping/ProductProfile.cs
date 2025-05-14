using Application.DTOs.Product;
using Application.Mapping.Resolvers;
using Domain.Entities;
using Domain.Enums;

namespace Application.Mapping;

public class ProductProfile: GenericProfile<ProductReadDto, ProductCreateDto, ProductUpdateDto, Product>
{
    public ProductProfile(): base()
    {
        CreateMap<Product, ProductReadDto>()
            .ForMember(dest => dest.ProductType, opt
                => opt.MapFrom(src => src.ProductType.ToString()))
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
            .ForMember(dest => dest.ImageUrl, opt =>
                opt.MapFrom<ProductImageResolver>());
        
        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.ProductType, opt =>
                opt.MapFrom(src => Enum.Parse<ProductType>(src.ProductType)))
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
        
        CreateMap<ProductUpdateDto, Product>()
            .ForMember(dest => dest.ProductType, opt =>
                opt.MapFrom(src => Enum.Parse<ProductType>(src.ProductType)))
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
    }
}