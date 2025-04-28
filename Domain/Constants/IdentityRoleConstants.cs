namespace Domain.Constants;

public static class IdentityRoleConstants
{
    public static readonly Guid AdminRoleGuid = new("fdf5ee34-bfc9-4fa3-a888-e820800e6121");
    public static readonly Guid UserRoleGuid = new("9ffc304f-1df2-4efe-af2f-726e1d7d1917");
    public static readonly Guid TrainerRoleGuid = new("2ec017dc-a27f-4277-bf37-90b5063b47cd");
    public static readonly Guid OwnerRoleGuid = new("f63cb887-a8f6-4465-8a49-e3274ce6af99");
    
    public const string Admin = "Admin";
    public const string User = "User";
    public const string Trainer = "Trainer";
    public const string Owner = "Owner";
}
