using Application.Abstracts.IRepositories;
using Application.Abstracts.IServices;
using Application.DTOs.Gym;
using Application.DTOs.GymStaff;
using Application.DTOs.User;
using Application.DTOs.UserGoal;
using Application.DTOs.UserMembership;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService: IUserService
{
    private readonly IGymService _gymService;
    private readonly ITrainerService _trainerService;
    private readonly IUserMembershipService _userMembershipService;
    private readonly IUserGoalService _userGoalService;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;

    public UserService(IGymService gymService, ITrainerService trainerService, IUserRepository userRepository,
        IUserMembershipService userMembershipService, UserManager<User> userManager, IUserGoalService userGoalService)
    {
        _gymService = gymService;
        _trainerService = trainerService;
        _userMembershipService = userMembershipService;
        _userManager = userManager;
        _userGoalService = userGoalService;
        _userRepository = userRepository;
    }

    public async Task<GymReadDto?> GetGymByUserId(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        var gymId = user?.GymId;
        if (gymId is null)
            return null;
        
        var gym = await _gymService.GetByIdAsync((Guid)gymId);
        
        return gym;
    }

    public async Task<GymStaffReadDto?> GetTrainerByUserId(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        var trainerId = user?.TrainerId;
        if (trainerId is null)
            return null;
        
        var trainer = await _trainerService.GetTrainerByIdAsync((Guid)trainerId);
        
        return trainer;
    }

    public async Task<UserMembershipReadDto?> GetActiveMembershipByGymId(Guid userId, Guid gymId)
    {
        return await _userMembershipService.GetUserActiveMembershipByUserIdAndGymIdAsync(userId, gymId);
    }

    public async Task<UserMetrics> GetUserMetricsAsync(Guid userId, DateTime date)
    {
        var user = await _userRepository.GetUserDetailsAsync(userId, date);
        if (user == null)
            return new UserMetrics();

        return new UserMetrics
        {
            WaterIntake = user.WaterIntakeLogs.Sum(w => w.VolumeMl),
            Steps = user.Steps.Sum(s => s.Steps),
            Calories = user.Meals.Sum(m => m.Calories),
            Weight = user.Weights.FirstOrDefault()?.WeightKg ?? 0,
            SleepMinutes = user.Sleeps.Sum(s =>
                (int)(s.SleepStart - s.WakeUpTime).TotalMinutes)
        };
    }

    public async Task<UserDetailsDto> GetUserDetailsAsync(Guid userId, Guid gymId)
    {
        const int previousDaysOffset = -1;
        var today = DateTime.Today;
        
        try 
        {
            var userMetrics = await GetUserMetricsAsync(userId, today);
            
            var membership = await GetActiveMembershipByGymId(userId, gymId);
            var membershipName = membership?.Membership.Name ?? string.Empty;
            
            var trainer = await GetTrainerByUserId(userId);
            var gym = await GetGymByUserId(userId);
            var userGoals = await _userGoalService.GetUserGoalsByUserIdAsync(userId);

            var userGoalReadDtos = userGoals.ToArray();

            var waterMl = userMetrics.WaterIntake;
            var waterGoal = GetGoalValue(userGoalReadDtos, Goal.Water);
            
            var sleepMinutes = userMetrics.SleepMinutes;
            
            var allSteps = userMetrics.Steps;
            var stepsGoal = GetGoalValue(userGoalReadDtos, Goal.Steps);
            
            var currentWeight = userMetrics.Weight;
            
            var calories = (int)userMetrics.Calories;
            var caloriesGoal = GetGoalValue(userGoalReadDtos, Goal.Calories);

           
            return new UserDetailsDto
            {
                Id = userId,
                Weight = currentWeight,
                SleepMinutes = sleepMinutes,
                WaterIntake = new DailyMetricDto(waterMl, waterGoal),
                Steps = new DailyMetricDto(allSteps, stepsGoal),
                Calories = new DailyMetricDto(calories, caloriesGoal),
                Gym = gym,
                Trainer = trainer,
                MembershipName = membershipName
            };
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error getting user details for user {userId}", ex);
        }
    }

    private static int GetGoalValue(IEnumerable<UserGoalReadDto> goals, Goal goalType) =>
        goals.FirstOrDefault(ug => Enum.Parse<Goal>(ug.GoalType) == goalType)?.Value ?? 0;
}