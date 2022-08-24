#pragma warning disable IDE0073 // The file header is missing or not located at the top of the file
using DataAccess.Models;
#pragma warning restore IDE0073 // The file header is missing or not located at the top of the file

namespace DataAccess.Data.IData;

public interface IUserData
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<IEnumerable<User>> GetUsersAsync();
    Task InsertUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid id);
    Task<User?> GetUserByUsernameAndEmailAsync(string username, string email);
    Task<User?> GetUserByCredentialsAsync(string nameEmail, string password);
}
