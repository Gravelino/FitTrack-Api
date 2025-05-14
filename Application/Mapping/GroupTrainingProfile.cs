using Application.DTOs.GroupTraining;
using Domain.Entities;

namespace Application.Mapping;

public class GroupTrainingProfile: GenericProfile<GroupTrainingReadDto, GroupTrainingCreateDto, 
    GroupTrainingUpdateDto, GroupTraining>
{
    public GroupTrainingProfile(): base()
    {
        
    }
}