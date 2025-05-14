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
    public DbSet<GymImage> GymImages { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<UserMembership> UserMemberships { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<TrainingTime> TrainingTimes { get; set; }
    
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

        builder.Entity<Admin>().Navigation(a => a.User).AutoInclude();
        
        builder.Entity<Trainer>().Navigation(t => t.User).AutoInclude();
        
        builder.Entity<Owner>().Navigation(o => o.User).AutoInclude();
        
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
            
            entity.Navigation(g => g.Images).AutoInclude();
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
        
        builder.Entity<UserMembership>()
            .HasKey(um => new { um.UserId, um.MembershipId });

        builder.Entity<UserMembership>()
            .HasOne(um => um.User)
            .WithMany(u => u.UserMemberships)
            .HasForeignKey(um => um.UserId);

        builder.Entity<UserMembership>()
            .HasOne(um => um.Membership)
            .WithMany(m => m.UserMemberships)
            .HasForeignKey(um => um.MembershipId);
        
        builder.Entity<GymFeedback>().Navigation(f => f.User).AutoInclude();
    }
}