using Application.Abstracts.IServices;
using Application.DTOs.GymStaff;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping.Resolvers;

public class TrainerImageResolver: IValueResolver<Trainer, GymStaffReadDto, string>
{
    private readonly IS3Service _s3Service;

    public TrainerImageResolver(IS3Service s3Service)
    {
        _s3Service = s3Service;
    }
    
    public string Resolve(Trainer source, GymStaffReadDto destination, string destMember, ResolutionContext context)
    {
        return !string.IsNullOrEmpty(source.User.PictureUrl)
            ? _s3Service.GeneratePreSignedUrl(source.User.PictureUrl, TimeSpan.FromMinutes(60))
            : string.Empty;
    }
}