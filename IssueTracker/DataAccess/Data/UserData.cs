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
        public Task<IEnumerable<User>> GetUsers()
        {
            return _db.LoadData<User, dynamic>("dbo.spUser_GetAll", new { });
        }
        public async Task<User?> GetUserById(Guid id)
        {
            var result = await _db.LoadData<User, dynamic>(
                "dbo.spUser_GetById", new { Id = id });
            return result.FirstOrDefault();
        }
        public Task InsertUser(User user)
        {
            return _db.SaveData("dbo.spUser_Insert", new { user.UserName, user.Email,user.Password });
        }
        public Task UpdateUser(User user)
        {
            return _db.SaveData("dbo.spUser_Update", user);
        }
        public Task DeleteUser(Guid id)
        {
            return _db.SaveData("dbo.spUser_Delete", new { Id = id });
        }
    }
}
