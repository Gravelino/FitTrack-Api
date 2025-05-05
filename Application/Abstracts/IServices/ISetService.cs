using Application.DTOs.Set;
using Domain.Entities;

namespace Application.Abstracts.IServices;

public interface ISetService : IService<SetReadDto, SetCreateDto, SetUpdateDto, Set>
{
    Task<IEnumerable<SetReadDto>> GetSetsInfoByIndividualTrainingId(Guid individualTrainingId);
}