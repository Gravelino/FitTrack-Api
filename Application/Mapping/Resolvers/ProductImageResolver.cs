using Application.Abstracts.IServices;
using Application.DTOs.Product;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping.Resolvers;

public class ProductImageResolver: IValueResolver<Product, ProductReadDto, string>
{
    private readonly IS3Service _s3Service;

    public ProductImageResolver(IS3Service s3Service)
    {
        _s3Service = s3Service;
    }
    
    public string Resolve(Product source, ProductReadDto destination, string destMember, ResolutionContext context)
    {
        return !string.IsNullOrEmpty(source.ImageUrl)
            ? _s3Service.GeneratePreSignedUrl(source.ImageUrl, TimeSpan.FromMinutes(60))
            : string.Empty;
    }
}