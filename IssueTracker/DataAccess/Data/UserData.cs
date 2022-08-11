using Dapper;
using DataAccess.Data.IData;
using DataAccess.DbAccess;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Data
{
    public class UserData : IUserData
    {
        private readonly ISQLDataAccess _db;
        private readonly IConfiguration _config;
        public UserData(ISQLDataAccess db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        public Task<IEnumerable<User>> GetUsersAsync()
        {
            return _db.LoadDataAsync<User>("dbo.spUser_GetAll");
        }
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var result = await _db.LoadDataAsync<User, dynamic>(
                "dbo.spUser_GetById", new { Id = id });
            return result.FirstOrDefault();
        }
        public async Task<User?> LoadUserDataAsync<T>(
            T parameters,
            string connectionId = "Default")
        {
            string storedProcedure = "dbo.spUser_AboutUser";
            //string storedProcedure =
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                IEnumerable<User?> users = await connection.QueryAsync<User, IEnumerable<Comment>, User?>(storedProcedure,
                    (user, comment)=> { user.Comments = comment; return user; },
                     parameters,
                     splitOn:"Id, Id",
                    commandType: CommandType.StoredProcedure);
                var user = users.FirstOrDefault();
                return user;
            }
        }
        public async Task<User?> GetUserByUsernameAndEmailAsync(string username, string email)
        {
            var result = await _db.LoadDataAsync<User, dynamic>(
                "dbo.spUser_GetByUsernameANDEmail", new { username, email  });
            return result.FirstOrDefault();
        }
        public async Task<User?> GetUserByCredentialsAsync(string nameEmail, string password)
        {
            var result = await _db.LoadDataAsync<User, dynamic>(
                "dbo.spUser_GetByCredentials", new { nameEmail, password });
            return result.FirstOrDefault();
        }
        public async Task<User?> GetAboutUserAsync(Guid id)
        {
            var result = await _db.LoadDataAsync<User, dynamic>(
                "dbo.spUser_AboutUser", new { Id = id });
            return result.FirstOrDefault();
        }
        public async Task InsertUserAsync(User user)
        {
            await _db.SaveDataAsync("dbo.spUser_Insert", new { user.UserName, user.Email,user.Password });
        }
        public async Task UpdateUserAsync(User user)
        {
            await _db.SaveDataAsync("dbo.spUser_Update", 
                new { Id=user.Id, UserName = user.UserName, Email = user.Email, Password = user.Password });
        }
        public async Task DeleteUserAsync(Guid id)
        {
            await _db.SaveDataAsync("dbo.spUser_Delete", new { Id = id });
        }
    }
}
