using Application.DTOs.Product;
using Domain.Entities;
using Domain.Enums;

namespace Application.Mapping;

public class ProductProfile: GenericProfile<ProductReadDto, ProductCreateDto, ProductUpdateDto, Product>
{
    public ProductProfile(): base()
    {
        CreateMap<Product, ProductReadDto>()
            .ForMember(dest => dest.ProductType, opt
                => opt.MapFrom(src => src.ProductType.ToString()));
        
        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.ProductType, opt =>
                opt.MapFrom(src => Enum.Parse<ProductType>(src.ProductType)));
        
        CreateMap<ProductUpdateDto, Product>()
            .ForMember(dest => dest.ProductType, opt =>
                opt.MapFrom(src => Enum.Parse<ProductType>(src.ProductType)));
    }
}