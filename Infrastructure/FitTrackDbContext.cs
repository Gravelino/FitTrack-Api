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
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Gym> Gyms { get; set; }
    
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
        
        builder.Entity<Trainer>()
            .HasMany(t => t.Customers)
            .WithOne(u => u.Trainer)
            .HasForeignKey(u => u.TrainerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Trainer>()
            .HasOne(t => t.User)
            .WithOne(u => u.TrainerProfile)
            .HasForeignKey<Trainer>(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Owner>()
            .HasOne(o => o.User)
            .WithOne(u => u.OwnerProfile)
            .HasForeignKey<Owner>(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Admin>()
            .HasOne(a => a.User)
            .WithOne(u => u.AdminProfile)
            .HasForeignKey<Admin>(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Gym>(entity =>
        {
            entity.OwnsOne(g => g.Address, address =>
            {
                address.Property(a => a.Country).HasMaxLength(100);
                address.Property(a => a.City).HasMaxLength(100);
                address.Property(a => a.Street).HasMaxLength(150).IsRequired();
                address.Property(a => a.Building).HasMaxLength(10);
                address.Property(a => a.ZipCode).HasMaxLength(20);
            });
        });
    }
}