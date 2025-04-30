namespace Domain.Requests;

public record RefreshTokenRequest
{
    public required string RefreshToken { get; init; }
}