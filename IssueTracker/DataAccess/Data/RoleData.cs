// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using DataAccess.Data.IData;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class RoleData : IRoleData
{
    private readonly ISQLDataAccess _db;
    public RoleData(ISQLDataAccess db)
    {
        _db = db;
    }
    public async Task<IEnumerable<Role>> GetAllAsync() =>
        await _db.LoadDataAsync<Role>("dbo.spRole_GetAll");

    public async Task<Role?> GetByIdAsync(int id) =>
        (await _db.LoadDataAsync<Role, dynamic>("dbo.spRole_Get", new { Id = id })).FirstOrDefault();
}
