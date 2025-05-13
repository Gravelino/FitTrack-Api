using Application.Abstracts.IRepositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository: Repository<Product>, IProductRepository
{
    public ProductRepository(FitTrackDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(string productType)
    {
        var products = await _context.Products
            .Where(p => p.ProductType == Enum.Parse<ProductType>(productType))
            .ToListAsync();
        
        return products;
    }
    
    public async Task<IEnumerable<Product>> GetProductByIdAsync(Guid id, string productType)
    {
        var products = await _context.Products
            .Where(p => p.Id == id && p.ProductType == Enum.Parse<ProductType>(productType))
            .ToListAsync();
        
        return products;
    }
    
    public async Task<IEnumerable<Product>> GetByGymIdAsync(Guid gymId, string productType)
    {
        var products = await _context.Products
            .Where(p => p.GymId == gymId && p.ProductType == Enum.Parse<ProductType>(productType))
            .ToListAsync();
        
        return products;
    }
}