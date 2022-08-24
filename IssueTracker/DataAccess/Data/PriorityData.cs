// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using DataAccess.Data.IData;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class PriorityData : IPriorityData
{
    private readonly ISQLDataAccess _db;
    public PriorityData(ISQLDataAccess db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Priority>> GetAllAsync() =>
        await _db.LoadDataAsync<Priority>("dbo.spPriority_GetAll");

    public async Task<Priority?> GetByIdAsync(int id) =>
        (await _db.LoadDataAsync<Priority, dynamic>("dbo.spPriority_Get", new { Id = id })).FirstOrDefault();
}
