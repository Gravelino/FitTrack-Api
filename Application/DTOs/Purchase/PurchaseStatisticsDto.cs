namespace Application.DTOs.Purchase;

public class PurchaseStatisticsDto
{
    public List<PurchaseStatistic> Memberships { get; set; }
    public List<PurchaseStatistic> Goods { get; set; }
    public List<PurchaseStatistic> Services { get; set; }
}

public record PurchaseStatistic(Guid GymId, string GymName, int TotalQuantity, decimal TotalIncome);