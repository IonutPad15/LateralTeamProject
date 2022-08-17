
namespace DataAccess.Models
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class Role
    {
        public RolesType Id { get; set; }
        public string Name { get; set; }

    }
    public enum RolesType
    {
        Developer=1,
        Tester=2,
        Owner=3,
        Collaborator=4
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
