#pragma warning disable IDE0073 // The file header is missing or not located at the top of the file
using DataAccess.Models;
#pragma warning restore IDE0073 // The file header is missing or not located at the top of the file

namespace DataAccess.Data.IData;

public interface IParticipantData
{
    Task AddAsync(Participant participant);
    Task DeleteAsync(int id);
    Task<IEnumerable<Participant>> GetAllAsync();
    Task<Participant?> GetByIdAsync(int id);
    Task<IEnumerable<Participant>> GetOwnersAndCollabsByProjectIdAsync(int id);
    Task<IEnumerable<Participant>> GetOwnerByProjectIdAsync(int id);
    Task UpdateAsync(Participant participant);
}
