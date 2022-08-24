﻿#pragma warning disable IDE0073 // The file header is missing or not located at the top of the file
using DataAccess.Models;
#pragma warning restore IDE0073 // The file header is missing or not located at the top of the file

namespace DataAccess.Data.IData;
public interface IPriorityData
{
    Task<IEnumerable<Priority>> GetAllAsync();
    Task<Priority?> GetByIdAsync(int id);
}
