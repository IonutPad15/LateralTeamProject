using DataAccess.Models;

namespace DataAccess.Repository;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<IEnumerable<User>> GetUsersAsync();
    Task InsertUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid id);
    Task<User?> GetUserByUsernameAndEmailAsync(string username, string email);
    Task<User?> GetUserByCredentialsAsync(string nameEmail, string password);
}
