namespace Domain.Requests;

public record DeleteByEmailRequest
{
    public required string Email { get; init; }
};