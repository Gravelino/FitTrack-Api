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
    
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Set> Sets { get; set; }
    
    public DbSet<IndividualTraining> IndividualTrainings { get; set; }
    public DbSet<GroupTraining> GroupTrainings { get; set; }
    
    public DbSet<StepsInfo> Steps { get; set; }
    public DbSet<Meal> Meals { get; set; }
    public DbSet<WeightsInfo> Weights { get; set; }
    public DbSet<Sleep> Sleeps { get; set; }
    
    public DbSet<WaterIntakeLog> WaterIntakeLogs { get; set; }
    public DbSet<UserGoal> UserGoals { get; set; }
    public DbSet<GymFeedback> GymFeedbacks { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<UserMembership> UserMemberships { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<TrainerComment> TrainerComments { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            
            entity.Property(u => u.FirstName).HasMaxLength(256);
            entity.Property(u => u.LastName).HasMaxLength(256);
        });
        
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

        builder.Entity<Trainer>(entity =>
        {
            entity.HasMany(t => t.Customers)
                .WithOne(u => u.Trainer)
                .HasForeignKey(u => u.TrainerId)
                .OnDelete(DeleteBehavior.SetNull);
            
            entity.HasOne(t => t.User)
                .WithOne(u => u.TrainerProfile)
                .HasForeignKey<Trainer>(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.Navigation(t => t.User).AutoInclude();
        });

        builder.Entity<Owner>(entity =>
        {
            entity.HasOne(o => o.User)
                .WithOne(u => u.OwnerProfile)
                .HasForeignKey<Owner>(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Navigation(o => o.User).AutoInclude();
        });
            
        builder.Entity<Admin>(entity =>
        {
            entity .HasOne(a => a.User)
                .WithOne(u => u.AdminProfile)
                .HasForeignKey<Admin>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Navigation(a => a.User).AutoInclude();
        });
        
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
        
        builder.Entity<UserGoal>(entity =>
        {
            entity.HasKey(g => g.Id);

            entity.Property(g => g.GoalType)
                .HasConversion<string>()
                .IsRequired();

            entity.Property(g => g.Value)
                .IsRequired();

            entity.HasOne(g => g.User)
                .WithMany(u => u.Goals)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(g => new { g.UserId, g.GoalType }).IsUnique();
        });

        builder.Entity<UserMembership>(entity =>
        {
            entity.HasKey(e => e.Id);
    
            entity.HasIndex(um => new { um.UserId, um.MembershipId });
            
            entity.HasIndex(um => um.ExpirationDate);

            entity.Property(um => um.PurchaseDate)
                  .IsRequired();

            entity.HasOne(um => um.User)
                  .WithMany(u => u.UserMemberships)
                  .HasForeignKey(um => um.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(um => um.Membership)
                  .WithMany(m => m.UserMemberships)
                  .HasForeignKey(um => um.MembershipId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(m => m.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entity.Property(m => m.Type)
                .IsRequired();
            
            entity.HasOne(m => m.Gym)
                .WithMany(g => g.Memberships)
                .HasForeignKey(m => m.GymId)
                .OnDelete(DeleteBehavior.Cascade);

        });

        builder.Entity<Product>(entity =>
        {
            entity.HasOne(p => p.Gym)
                .WithMany(g => g.Products)
                .HasForeignKey(p => p.GymId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Purchase>(entity =>
        {
            entity.Property(p => p.Price)
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();

            entity.Property(p => p.PurchaseDate)
                  .IsRequired();

            entity.HasIndex(p => p.PurchaseDate);

            entity.HasOne(p => p.User)
                  .WithMany(u => u.Purchases)
                  .HasForeignKey(p => p.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(p => p.Product)
                  .WithMany()
                  .HasForeignKey(p => p.ProductId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(p => p.Gym)
                  .WithMany()
                  .HasForeignKey(p => p.GymId)
                  .OnDelete(DeleteBehavior.Cascade);

            // entity.Navigation(p => p.Product).AutoInclude();
            // entity.Navigation(p => p.Gym).AutoInclude();
        });
        
        builder.Entity<GymFeedback>().Navigation(f => f.User).AutoInclude();
        
        builder.Entity<TrainerComment>(entity =>
            {
                entity.HasOne(tc => tc.User)
                    .WithMany(u => u.TrainerComments)
                      .HasForeignKey(tc => tc.UserId)
                      .OnDelete(DeleteBehavior.Restrict); 
                
                entity.HasOne(tc => tc.Trainer)
                      .WithMany(t => t.TrainerComments)
                      .HasForeignKey(tc => tc.TrainerId)
                      .OnDelete(DeleteBehavior.Restrict); 
                
                entity.Navigation(tc => tc.Trainer).AutoInclude();
                entity.Navigation(tc => tc.User).AutoInclude();
            });

        builder.Entity<GroupTraining>(entity =>
        {
            entity.Navigation(g => g.Gym).AutoInclude();
            entity.Navigation(g => g.Trainer).AutoInclude();
        });
    }
}