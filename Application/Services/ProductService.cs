using Application.Abstracts;
using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.Product;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class ProductService: Service<ProductReadDto, ProductCreateDto, ProductUpdateDto, Product>, IProductService
{
    private readonly IProductRepository _repository;
    private readonly IS3Service _s3Service;

    public ProductService(IProductRepository repository, IMapper mapper, IS3Service s3Service) : base(repository, mapper)
    {
        _repository = repository;
        _s3Service = s3Service;
    }

    public async Task<IEnumerable<ProductReadDto>> GetAllProductsAsync(string productType)
    {
        var products = await _repository.GetAllProductsAsync(productType);
        return _mapper.Map<IEnumerable<ProductReadDto>>(products);
    }
    
    public async Task<IEnumerable<ProductReadDto>> GetProductsByGymId(Guid gymId, string productType)
    {
        var products = await _repository.GetByGymIdAsync(gymId, productType);
        return _mapper.Map<IEnumerable<ProductReadDto>>(products);
    }

    public async Task<Guid> CreateGoodAsync(ProductCreateDto dto, IFormFile image)
    {
        var product = _mapper.Map<Product>(dto);
        product.Id = Guid.NewGuid();
        
        if (image is not null)
        {
            product.ImageUrl = await _s3Service.UploadFileAsync(image, "products", product.Id);
        }
        
        return await _repository.AddAsync(product);
    }

    public async Task UpdateGoodAsync(Guid id, ProductUpdateDto dto, IFormFile image)
    {
        var product = await _repository.GetByIdAsync(id);
        if(product is null)
            return;
        
        _mapper.Map(dto, product);
        
        if (image is not null)
        {
            product.ImageUrl = await _s3Service.UploadFileAsync(image, "products", product.Id);
        }
        
        await _repository.UpdateAsync(id, product);
    }
}