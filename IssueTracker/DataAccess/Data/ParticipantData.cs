using Dapper;
using DataAccess.Data.IData;
using DataAccess.DbAccess;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ParticipantData : IParticipantData
    {
        private readonly ISQLDataAccess _db;
        private readonly IConfiguration _config;
        public ParticipantData(ISQLDataAccess db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task AddAsync(Participant participant) =>
            await _db.SaveDataAsync("dbo.spParticipant_Insert",
                new { participant.UserId, participant.ProjectId, participant.RoleId });

        public async Task<IEnumerable<Participant>> GetAllAsync() =>
            await _db.LoadDataAsync<Participant>("dbo.spParticipant_GetAll");

        public async Task<Participant?> GetByIdAsync(int id) =>
            (await _db.LoadDataAsync<Participant, dynamic>("dbo.spParticipant_Get", new { Id = id })).FirstOrDefault();

        public async Task UpdateAsync(Participant participant) =>
            await _db.SaveDataAsync("dbo.spParticipant_Update", new { participant.Id, participant.RoleId });
        public async Task<IEnumerable<Participant>> GetByProjectIdAsync(string storedProcedure, int id, string connectionId = "Default")
        {
            var param = new
            {
                ProjectId = id
            };
            List<Participant> participants = new List<Participant>();
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                await connection.QueryAsync<Participant, User, Role, Participant>
                    (storedProcedure,
                    map: (first, second, third) =>
                    {
                        first.User = second;
                        first.Role = third;
                        participants.Add(first);
                        return first;
                    }, param, commandType:CommandType.StoredProcedure);

            }
            return participants;
        }
        public async Task DeleteAsync(int id) =>
            await _db.SaveDataAsync("dbo.spParticipant_Delete", new { Id = id });
    }
}
