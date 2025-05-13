using Domain.Entities;

namespace Application.Abstracts.IRepositories;

public interface IProductRepository: IRepository<Product>
{
    Task<IEnumerable<Product>> GetAllProductsAsync(string productType);
    Task<IEnumerable<Product>> GetByGymIdAsync(Guid gymId, string productType);
}