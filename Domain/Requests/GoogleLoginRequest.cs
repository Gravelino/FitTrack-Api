namespace Domain.Requests;

public record GoogleLoginRequest
{
    public required string IdToken { get; init; }
}