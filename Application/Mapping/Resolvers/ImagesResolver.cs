using Application.Abstracts.IServices;
using Application.DTOs;
using Application.DTOs.Gym;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping.Resolvers;

public class ImagesResolver : IValueResolver<Gym, GymReadDto, ICollection<S3ObjectDto>>
{
    private readonly IS3Service _s3Service;

    public ImagesResolver(IS3Service s3Service)
    {
        _s3Service = s3Service;
    }

    public ICollection<S3ObjectDto> Resolve(Gym source, GymReadDto destination, ICollection<S3ObjectDto> destMember, ResolutionContext context)
    {
        if (source.Images is null)
        {
            return new List<S3ObjectDto>();
        }
        
        return source.Images.Select(image => new S3ObjectDto
        {
            Name = Path.GetFileName(image.ImageUrl),
            PreSignedUrl = _s3Service.GeneratePreSignedUrl(image.ImageUrl, TimeSpan.FromMinutes(60))
        }).ToList();
    }
}