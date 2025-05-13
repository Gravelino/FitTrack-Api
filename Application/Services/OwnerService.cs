using Application.Abstracts;
using Application.Abstracts.IServices;
using Application.DTOs;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class OwnerService : IOwnerService
{
    private readonly IUnitOfWork _unitOfWork;


    public OwnerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateOwnerProfileAsync(User user)
    {
        var ownerProfile = new Owner
        {
            UserId = user.Id,
        };
            
        user.OwnerProfile = ownerProfile;
        
        await _unitOfWork.Owners.AddAsync(ownerProfile);
        await _unitOfWork.SaveChangesAsync();
    }
}