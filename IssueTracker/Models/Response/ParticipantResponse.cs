namespace Models.Response;
public class ParticipantResponse
{
    public int Id { get; set; }
    public UserResponse? User { get; set; }
    public RoleResponse? Role { get; set; }
    public ProjectResponse? Project { get; set; }
}
