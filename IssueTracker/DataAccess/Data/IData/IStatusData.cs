﻿using DataAccess.Models;

namespace DataAccess.Data.IData;

public interface IStatusData
{
    Task<IEnumerable<Status>> GetAllAsync();
    Task<Status?> GetByIdAsync(int id);
}