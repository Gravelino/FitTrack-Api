using Application.Abstracts.IServices;
using Application.Mapping.Resolvers;
using Application.Services;

namespace Api.Extensions;

public static class ApplicationServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IService<,,,>), typeof(Service<,,,>));
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IOwnerService, OwnerService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<ISetService, SetService>();
        services.AddScoped<IIndividualTrainingService, IndividualTrainingService>();
        services.AddScoped<IMealService, MealService>();
        services.AddScoped<ICalorieStatisticsService, CalorieStatisticsService>();
        services.AddScoped<IStepsInfoService, StepsInfoService>();
        services.AddScoped<IWeightsInfoService, WeightsInfoService>();
        services.AddScoped<IUserGoalService, UserGoalService>();
        services.AddScoped<ISleepService, SleepService>();
        services.AddScoped<ISleepStatisticService, SleepStatisticService>();
        services.AddScoped<IWaterIntakeLogService, WaterIntakeLogService>();
        services.AddScoped<IGymService, GymService>();
        services.AddScoped<IS3Service, S3Service>();
        services.AddScoped<IMembershipService, MembershipService>();
        services.AddScoped<IGymFeedbackService, GymFeedbackService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ITrainerCommentService, TrainerCommentService>();
        services.AddScoped<IGroupTrainingService, GroupTrainingService>();
        services.AddScoped<IPurchaseService, PurchaseService>();
        services.AddScoped<IUserMembershipService, UserMembershipService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPurchaseStatisticsService, PurchaseStatisticsService>();
        services.AddScoped<IUserStatisticService, UserStatisticService>();
        services.AddScoped<IGeneralStatisticService, GeneralStatisticService>();
        
        services.AddScoped<MainImageUrlResolver>();
        services.AddScoped<ProductImageResolver>();
    }
}
