﻿using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using DataAccess.Utils;

namespace DataAccess.DbAccess
{
    public class SQLDataAccess : ISQLDataAccess
    {
        private readonly IConfiguration _config;
        public SQLDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadDataAsync<T>(
            string storedProcedure,
            string connectionId = "Default")
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                return await connection.QueryAsync<T>(storedProcedure,
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<IEnumerable<T>> LoadDataAsync<T, U>(
            string storedProcedure,
            U parameters,
            string connectionId = "Default")
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                return await connection.QueryAsync<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
        //public async Task<IEnumerable<T>> LoadDataAsync<T,V, U>(
        //    string storedProcedure,
        //    U parameters,
        //    string connectionId = "Default")
        //{
        //    using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
        //    {
        //        var result =  await connection.QueryAsync<T,V>(storedProcedure, parameters,
        //            commandType: CommandType.StoredProcedure);
        //    }
        //}
        public async Task SaveDataAsync<T>(
            string storedProcedure,
            T parameters,
            string connectionId = "Default")
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
                SqlMapper.AddTypeHandler(new GuidHandler());
                SqlMapper.AddTypeHandler(new TimeSpanHandler());
                await connection.ExecuteAsync(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
