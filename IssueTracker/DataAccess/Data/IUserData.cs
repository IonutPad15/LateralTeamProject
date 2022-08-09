using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IUserData
    {
        Task<User?> GetUserById(Guid id);
        Task<IEnumerable<User>> GetUsers();
        Task InsertUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(Guid id);
    }
}