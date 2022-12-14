namespace DataAccess.Models;
public class User
{
    public Guid Id { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string UserName { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public bool IsDeleted { get; set; }
    public IEnumerable<Comment> Comments { get; set; } = Enumerable.Empty<Comment>();
}
