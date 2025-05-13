namespace Domain.Requests;

public record LoginRequest
{
    public required string Login { get; init; }
    public required string Password { get; init; }
}