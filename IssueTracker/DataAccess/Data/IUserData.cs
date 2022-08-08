using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IUserData
    {
        Task<User?> GetUser(Guid id);
        Task<IEnumerable<User>> GetUsers();
        Task InsertUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(Guid id);
    }
}