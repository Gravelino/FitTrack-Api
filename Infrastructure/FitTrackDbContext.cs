using Domain.Constants;
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
        
        builder.Entity<IdentityRole<Guid>>()
            .HasData(new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid>
                {
                    Id = IdentityRoleConstants.AdminRoleGuid,
                    Name = IdentityRoleConstants.Admin,
                    NormalizedName = IdentityRoleConstants.Admin.ToUpper(),
                },
                new IdentityRole<Guid>
                {
                    Id = IdentityRoleConstants.UserRoleGuid,
                    Name = IdentityRoleConstants.User,
                    NormalizedName = IdentityRoleConstants.User.ToUpper(),
                },
                new IdentityRole<Guid>
                {
                    Id = IdentityRoleConstants.TrainerRoleGuid,
                    Name = IdentityRoleConstants.Trainer,
                    NormalizedName = IdentityRoleConstants.Trainer.ToUpper(),
                },
                new IdentityRole<Guid>
                {
                    Id = IdentityRoleConstants.OwnerRoleGuid,
                    Name = IdentityRoleConstants.Owner,
                    NormalizedName = IdentityRoleConstants.Owner.ToUpper(),
                }
            });
    }
}