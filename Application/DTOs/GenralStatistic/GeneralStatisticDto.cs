namespace Application.DTOs.GenralStatistic;

public class GeneralStatisticDto
{
    public GymStaffDto GymStaff { get; set; }
    public UsersStatisticDto Users { get; set; }
    public PurchasesStatisticDto Purchases { get; set; }
    public int? GymsCount { get; set; }
}