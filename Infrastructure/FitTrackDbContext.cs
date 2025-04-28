using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class FitTrackDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public FitTrackDbContext(DbContextOptions<FitTrackDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<User>()
            .Property(u => u.FirstName).HasMaxLength(256);
        
        builder.Entity<User>()
            .Property(u => u.LastName).HasMaxLength(256);
    }
}