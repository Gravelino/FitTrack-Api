namespace Application.DTOs.User;

public class CurrentUserDto
{
    public Guid Id { get; set; } = default!;
    public string Login { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? PictureUrl { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
    public Guid? GymId { get; set; }
}
