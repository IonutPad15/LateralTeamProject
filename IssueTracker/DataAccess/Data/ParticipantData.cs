using DataAccess.Data.IData;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class ParticipantData : IParticipantData
{
    private readonly ISQLDataAccess _db;
    public ParticipantData(ISQLDataAccess db)
    {
        _db = db;
    }

    public async Task AddAsync(Participant participant) =>
        await _db.SaveDataAsync("dbo.spParticipant_Insert",
            new { participant.UserId, participant.ProjectId, participant.RoleId });

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
        await _db.SaveDataAsync("dbo.spParticipant_Update", new { participant.Id, participant.RoleId });
    }

    public async Task DeleteAsync(int id)
    {
        await _db.SaveDataAsync("dbo.spParticipant_Delete", new { Id = id });
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
