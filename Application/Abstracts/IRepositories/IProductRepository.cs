using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IProductRepository: IRepository<Product>
{
    Task<IEnumerable<Product>> GetByGymIdAsync(Guid gymId, string productType);
}