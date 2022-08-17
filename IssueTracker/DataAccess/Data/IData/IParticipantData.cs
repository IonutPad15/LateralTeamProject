﻿using DataAccess.Models;

namespace DataAccess.Data.IData
{
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
}