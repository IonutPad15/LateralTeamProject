using System.Data.SqlClient;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Repository;

public class ParticipantRepository : IParticipantRepository
{
    private readonly ISQLDataAccess _db;
    public ParticipantRepository(ISQLDataAccess db)
    {
        _db = db;
    }

    public async Task<int> AddAsync(Participant participant)
    {
        var result = await _db.SaveDataAndGetIdAsync<object, int>("dbo.spParticipant_Insert",
            new { participant.UserId, participant.ProjectId, participant.RoleId });
        return result;
    }

    public async Task<IEnumerable<Participant>> GetAllAsync()
    {
        return await _db.LoadDataAsync<Participant>("dbo.spParticipant_GetAll");
    }

    public async Task<Participant?> GetByIdAsync(int id)
    {
        return (await _db.LoadDataAsync<Participant, dynamic>("dbo.spParticipant_Get", new { Id = id })).FirstOrDefault();
    }

    public async Task UpdateAsync(Participant participant)
    {
        try
        {
            await _db.SaveDataAsync("dbo.spParticipant_Update", new { participant.Id, participant.RoleId });
        }
        catch (SqlException ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            await _db.SaveDataAsync("dbo.spParticipant_Delete", new { Id = id });
        }
        catch (SqlException ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }

    public async Task<IEnumerable<Participant>> GetOwnersAndCollabsByProjectIdAsync(int id)
    {
        return await _db.GetByProjectIdAsync("spParticipant_GetOwnersAndCollabsByProjectId", id);
    }

    public async Task<IEnumerable<Participant>> GetOwnerByProjectIdAsync(int id)
    {
        return await _db.GetByProjectIdAsync("spParticipant_GetOwner", id);
    }
}
