using DataAccess.Repository;

namespace DataAccess.Utils;

public class CheckRole
{

    public async static Task<bool> IsOwner(IParticipantRepository _participantData, Guid userid, int projectId)
    {
        var results = await _participantData.GetOwnerByProjectIdAsync(projectId);
        if (results == null) return false;
        if (!results.Any()) return false;
        var result = results.FirstOrDefault(x => x.UserId == userid);
        if (result == null) return false;
        return true;

    }
    public async static Task<bool> OwnerExists(IParticipantRepository _participantData, int projectId)
    {
        var results = await _participantData.GetOwnerByProjectIdAsync(projectId);
        if (!results.Any()) return false;
        return true;
    }
    public async static Task<bool> IsOwnerOrColllab(IParticipantRepository _participantData, Guid userid, int projectId)
    {
        var results = await _participantData.GetOwnersAndCollabsByProjectIdAsync(projectId);
        var result = results?.FirstOrDefault(x => x.UserId == userid);
        if (result == null) return false;
        return true;
    }
}
