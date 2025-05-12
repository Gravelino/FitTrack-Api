using Application.Mapping;

namespace Api.Extensions;

public static class AutoMapperExtensions
{
    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(ExerciseProfile),
            typeof(SetProfile),
            typeof(IndividualTrainingProfile),
            typeof(MealProfile),
            typeof(UserGoalProfile),
            typeof(WeightsInfoProfile),
            typeof(StepsInfoProfile),
            typeof(SleepProfile),
            typeof(WaterIntakeLogProfile),
            typeof(GymProfile),
            typeof(MembershipProfile)
        );

        return services;
    }
}
