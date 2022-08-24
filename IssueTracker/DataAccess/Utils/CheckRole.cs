#pragma warning disable IDE0073 // The file header is missing or not located at the top of the file
using DataAccess.Data.IData;
#pragma warning restore IDE0073 // The file header is missing or not located at the top of the file

namespace DataAccess.Utils;

public class CheckRole
{

    public async static Task<bool> IsOwner(IParticipantData _participantData, Guid userid, int projectId)
    {
        var results = await _participantData.GetOwnerByProjectIdAsync(projectId);
        if (results == null) return false;
        if (!results.Any()) return false;
        var result = results.FirstOrDefault(x => x.UserId == userid);
        if (result == null) return false;
        return true;

    }
    public async static Task<bool> OwnerExists(IParticipantData _participantData, int projectId)
    {
        var results = await _participantData.GetOwnerByProjectIdAsync(projectId);
        if (!results.Any()) return false;
        return true;
    }
    public async static Task<bool> IsOwnerOrColllab(IParticipantData _participantData, Guid userid, int projectId)
    {
        var results = await _participantData.GetOwnersAndCollabsByProjectIdAsync(projectId);
        var result = results?.FirstOrDefault(x => x.UserId == userid);
        if (result == null) return false;
        return true;

    }
}
