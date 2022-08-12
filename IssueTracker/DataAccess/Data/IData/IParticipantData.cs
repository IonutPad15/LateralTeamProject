using DataAccess.Models;

namespace DataAccess.Data.IData
{
    public interface IParticipantData
    {
        Task AddAsync(Participant participant);
        Task DeleteAsync(int id);
        Task<IEnumerable<Participant>> GetAllAsync();
        Task<Participant?> GetByIdAsync(int id);
        Task UpdateAsync(Participant participant);
        Task<IEnumerable<Participant>> GetByProjectIdAsync(int id, string connectionId = "Default");
    }
}