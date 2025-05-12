using Application.Abstracts.IServices;
using Application.DTOs.Gym;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping.Resolvers;

public class MainImageUrlResolver : IValueResolver<Gym, GymReadDto, string>
{
    private readonly IS3Service _s3Service;

    public MainImageUrlResolver(IS3Service s3Service)
    {
        _s3Service = s3Service;
    }

    public string Resolve(Gym source, GymReadDto destination, string destMember, ResolutionContext context)
    {
        return !string.IsNullOrEmpty(source.MainImageUrl)
            ? _s3Service.GeneratePreSignedUrl(source.MainImageUrl, TimeSpan.FromMinutes(60))
            : string.Empty;
    }
}
