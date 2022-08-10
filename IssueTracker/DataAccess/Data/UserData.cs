using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class UserData : IUserData
    {
        private readonly ISQLDataAccess _db;
        public UserData(ISQLDataAccess db)
        {
            _db = db;
        }
        public Task<IEnumerable<User>> GetUsersAsync()
        {
            return _db.LoadDataAsync<User, dynamic>("dbo.spUser_GetAll", new { });
        }
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var result = await _db.LoadDataAsync<User, dynamic>(
                "dbo.spUser_GetById", new { Id = id });
            return result.FirstOrDefault();
        }
        public async Task<User?> GetUserByUsernameAndEmailAsync(string username, string email)
        {
            var result = await _db.LoadDataAsync<User, dynamic>(
                "dbo.spUser_GetByUsernameANDEmail", new { username, email  });
            return result.FirstOrDefault();
        }
        public async Task InsertUserAsync(User user)
        {
            await _db.SaveDataAsync("dbo.spUser_Insert", new { user.UserName, user.Email,user.Password });
        }
        public async Task UpdateUserAsync(User user)
        {
            await _db.SaveDataAsync("dbo.spUser_Update", user);
        }
        public async Task DeleteUserAsync(Guid id)
        {
            await _db.SaveDataAsync("dbo.spUser_Delete", new { Id = id });
        }
    }
}
