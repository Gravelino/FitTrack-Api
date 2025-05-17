using Application.Abstracts.IRepositories;
using Infrastructure.Repositories;

namespace Api.Extensions;

public static class RepositoryServiceExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<ITrainerRepository, TrainerRepository>();
        services.AddScoped<IIndividualTrainingRepository, IndividualTrainingRepository>();
        services.AddScoped<ISetRepository, SetRepository>();
        services.AddScoped<IMealRepository, MealRepository>();
        services.AddScoped<IWeightsInfoRepository, WeightsInfoRepository>();
        services.AddScoped<IStepsInfoRepository, StepsInfoRepository>();
        services.AddScoped<IUserGoalRepository, UserGoalRepository>();
        services.AddScoped<ISleepRepository, SleepRepository>();
        services.AddScoped<IWaterIntakeLogRepository, WaterIntakeLogRepository>();
        services.AddScoped<IGymRepository, GymRepository>();
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IMembershipRepository, MembershipRepository>();
        services.AddScoped<IGymFeedbackRepository, GymFeedbackRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITrainerCommentRepository, TrainerCommentRepository>();
        services.AddScoped<IGroupTrainingRepository, GroupTrainingRepository>();
        services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        services.AddScoped<IUserMembershipRepository, UserMembershipRepository>();
    }
}