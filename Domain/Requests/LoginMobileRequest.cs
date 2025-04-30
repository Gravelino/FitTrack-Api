using Domain.Enums;

namespace Domain.Requests;

public record LoginMobileRequest
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public string? ProfilePicture { get; init; }
    public required Role Role { get; init; }
}