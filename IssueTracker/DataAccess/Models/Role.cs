#pragma warning disable IDE0073 // The file header is missing or not located at the top of the file

namespace DataAccess.Models;
#pragma warning restore IDE0073 // The file header is missing or not located at the top of the file

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class Role
{
    public RolesType Id { get; set; }
    public string Name { get; set; }

}
public enum RolesType
{
    Developer = 1,
    Tester = 2,
    Owner = 3,
    Collaborator = 4
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
