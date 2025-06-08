namespace Application.DTOs.Purchase;

public class PurchaseStatisticsGroupedDto
{
    public List<PurchaseStatisticGrouped> Memberships { get; set; }
    public List<PurchaseStatisticGrouped> Goods { get; set; }
    public List<PurchaseStatisticGrouped> Services { get; set; }
}

public record PurchaseStatisticGrouped(string Period, int TotalQuantity, decimal TotalIncome);

public enum PurchasesGroupBy
{
    Day = 1,
    Month = 2
}