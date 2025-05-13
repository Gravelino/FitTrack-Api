using Application.DTOs.Product;
using Domain.Entities;

namespace Application.Mapping;

public class ProductProfile: GenericProfile<ProductReadDto, ProductCreateDto, ProductUpdateDto, Product>
{
    public ProductProfile():base()
    {
        
    }
}