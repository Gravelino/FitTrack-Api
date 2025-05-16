using Application.DTOs.Product;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Abstracts.IServices;

public interface IProductService: IService<ProductReadDto, ProductCreateDto, ProductUpdateDto, Product>
{
    Task<IEnumerable<ProductReadDto>> GetAllProductsAsync(string productType);
    Task<IEnumerable<ProductReadDto>> GetProductsByGymId(Guid gymId, string productType);
    Task<Guid> CreateGoodAsync(ProductCreateDto dto, IFormFile image);
    Task UpdateGoodAsync(Guid id, ProductUpdateDto dto, IFormFile? image);
}