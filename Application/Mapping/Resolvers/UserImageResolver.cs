using Application.Abstracts.IServices;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping.Resolvers;

public class UserImageResolver: IValueResolver<User, CurrentUserDto, string>
{
    private readonly IS3Service _s3Service;

    public UserImageResolver(IS3Service s3Service)
    {
        _s3Service = s3Service;
    }
    
    public string Resolve(User source, CurrentUserDto destination, string destMember, ResolutionContext context)
    {
        return !string.IsNullOrEmpty(source.PictureUrl)
            ? _s3Service.GeneratePreSignedUrl(source.PictureUrl, TimeSpan.FromMinutes(60))
            : string.Empty;
    }
}