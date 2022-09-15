using System.Data.SqlClient;
using DataAccess.DbAccess;
using DataAccess.Models;
namespace DataAccess.Repository;

public class UserRepository : IUserRepository
{
    private readonly ISQLDataAccess _db;
    public UserRepository(ISQLDataAccess db)
    {
        _db = db;
    }
    public Task<IEnumerable<User>> GetUsersAsync()
    {
        return _db.LoadDataAsync<User>("dbo.spUser_GetAll");
    }
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        var result = await _db.LoadDataAsync<User, object>(
            "dbo.spUser_GetById", new { Id = id });
        return result.FirstOrDefault();
    }
    public async Task<User?> GetUserByUsernameAndEmailAsync(string username, string email)
    {
        var result = await _db.LoadDataAsync<User, object>(
            "dbo.spUser_GetByUsernameANDEmail", new { username, email });
        return result.FirstOrDefault();
    }
    public async Task<User?> GetUserByCredentialsAsync(string nameEmail, string password)
    {
        var result = await _db.LoadDataAsync<User, object>(
            "dbo.spUser_GetByCredentials", new { nameEmail, password });
        return result.FirstOrDefault();
    }
    public async Task InsertUserAsync(User user)
    {
        await _db.SaveDataAsync("dbo.spUser_Insert", new { user.UserName, user.Email, user.Password });
    }
    public async Task UpdateUserAsync(User user)
    {
        try
        {
            await _db.SaveDataAsync("dbo.spUser_Update",
            new { Id = user.Id, UserName = user.UserName, Email = user.Email, Password = user.Password });
        }
        catch (SqlException ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }
    public async Task DeleteUserAsync(Guid id)
    {
        try
        {
            await _db.SaveDataAsync("dbo.spUser_Delete", new { Id = id });
        }
        catch (SqlException ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }
}
