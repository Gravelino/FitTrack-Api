namespace Application.DTOs.Purchase;

public class PurchaseStatisticsDto
{
    public PurchaseStatistic Memberships { get; set; }
    public PurchaseStatistic Goods { get; set; }
    public PurchaseStatistic Services { get; set; }
}

public class PurchaseStatistic
{
    public int TotalQuantity { get; set; }
    public decimal TotalIncome { get; set; }
}