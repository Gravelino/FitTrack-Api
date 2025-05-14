using Application.DTOs.TrainerComment;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface ITrainerCommentService: IService<TrainerCommentReadDto, TrainerCommentCreateDto,
    TrainerCommentUpdateDto, TrainerComment>
{
    Task<IEnumerable<TrainerCommentReadDto>> GetTrainerCommentsByUserIdAndDate(Guid userId, DateTime date);
    Task<IEnumerable<TrainerCommentReadDto>> GetTrainerCommentsByTrainerIdAndDate(Guid trainerId, DateTime date);
}