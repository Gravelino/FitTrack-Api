using Application.Mapping;

namespace Api.Extensions;

public static class AutoMapperExtensions
{
    public static void AddAutoMapperProfiles(this IServiceCollection services)
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
            typeof(MembershipProfile),
            typeof(AdminProfile),
            typeof(TrainerProfile),
            typeof(GymFeedbackProfile),
            typeof(UserProfile),
            typeof(ProductProfile),
            typeof(TrainerCommentProfile),
            typeof(GroupTrainingProfile),
            typeof(PurchaseProfile),
            typeof(UserMembershipProfile)
        );
    }
}
