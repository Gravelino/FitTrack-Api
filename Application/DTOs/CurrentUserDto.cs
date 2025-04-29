namespace Application.DTOs;

public class CurrentUserDto
{
    public Guid Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? PictureUrl { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
}
