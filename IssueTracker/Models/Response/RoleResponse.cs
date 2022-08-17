namespace Models.Response
{
    public class RoleResponse
    {
        public RoleResponse(RoleType id, string name)
        {
            Id = id;
            Name = name;
        }

        public RoleType Id { get; set; }
       public string Name { get; set; }
    }
    public enum RoleType
    {
        Developer = 1,
        Tester = 2,
        Owner = 3, 
        Collaborator = 4
    }
}