namespace Models.Response
{
    public class RoleResponse
    {
       public RolesInfo Id { get; set; }
       public string? Name { get; set; }
    }
    public enum RolesInfo
    {
        Developer = 1, Tester = 2, Owner = 3, Collaborator = 4
    }
}