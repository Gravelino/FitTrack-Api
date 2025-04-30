using Application.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly FitTrackDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(FitTrackDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<Guid> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(Guid id, T entity)
    {
        if (entity.Id != id)
        {
            throw new ArgumentException("Ids do not match");
        }
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}