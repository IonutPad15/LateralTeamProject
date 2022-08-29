namespace Models.Response;
public class UserResponse
{
    public UserResponse(Guid id, string userName, string email)
    {
        Id = id;
        UserName = userName;
        Email = email;
    }

    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}
