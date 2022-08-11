using DataAccess.Models;

namespace DataAccess.Data.IData
{
    public interface IUserData
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task InsertUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
        Task<User?> GetUserByUsernameAndEmailAsync(string username, string email);
        Task<User?> GetUserByCredentialsAsync(string nameEmail, string password);
        Task<User?> GetAboutUserAsync(Guid id);
        Task<User?> LoadUserDataAsync<T>(T parameters, string connectionId = "Default");
    }
}