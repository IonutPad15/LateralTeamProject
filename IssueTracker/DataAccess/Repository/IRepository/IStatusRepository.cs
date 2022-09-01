﻿using DataAccess.Models;

namespace DataAccess.Repository;

public interface IStatusRepository
{
    Task<IEnumerable<Status>> GetAllAsync();
    Task<Status?> GetByIdAsync(int id);
}
