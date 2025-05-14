using Application.DTOs.TrainerComment;
using Domain.Entities;

namespace Application.Mapping;

public class TrainerCommentProfile: GenericProfile<TrainerCommentReadDto, TrainerCommentCreateDto,
    TrainerCommentUpdateDto, TrainerComment>
{
    public TrainerCommentProfile(): base()
    {
        
    }
}