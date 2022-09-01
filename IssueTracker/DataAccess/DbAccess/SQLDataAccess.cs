using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using DataAccess.Utils;
using DataAccess.Models;

namespace DataAccess.DbAccess;

public class SQLDataAccess : ISQLDataAccess
{
    private readonly IConfiguration _config;
    public SQLDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst>(
        string storedProcedure,
        string connectionId = "Default")
    {
        using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            return await connection.QueryAsync<TFirst>(storedProcedure,
                commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<IEnumerable<Participant>> GetByProjectIdAsync(string storedProcedure, int id, string connectionId = "Default")
    {
        var param = new
        {
            ProjectId = id
        };
        List<Participant> participants = new List<Participant>();
        using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            await connection.QueryAsync<Participant, User, Role, Participant>
                (storedProcedure,
                map: (first, second, third) =>
                {
                    first.User = second;
                    first.Role = third;
                    participants.Add(first);
                    return first;
                }, param, commandType: CommandType.StoredProcedure);

        }
        return participants;
    }
    public async Task<IEnumerable<Comment>> LoadCommentDataAsync<TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
    {
        List<Comment> comments = new List<Comment>();
        using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            var results = await connection.QueryMultipleAsync
                (storedProcedure, parameter, commandType: CommandType.StoredProcedure);
            comments = results.Read<Comment>().ToList();
            var files = results.Read<Models.File>();
            List<Models.File> tempfiles = new List<Models.File>();
            for (int i = 0; i < comments.Count(); ++i)
            {
                tempfiles = files.Where(x => x.FileCommentId == comments[i].Id).ToList();
                comments[i].MetaDatas = tempfiles;
            }
        }
        var result = comments.AsEnumerable();
        return result;
    }
    public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TParameter>(
        string storedProcedure,
        TParameter parameter,
        string connectionId = "Default")
    {
        using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            return await connection.QueryAsync<TFirst>(storedProcedure, parameter,
                commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond>(string storedProcedure, string connectionId = "Default")
        where TFirst : class where TSecond : class
    {
        using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            return await con.QueryAsync<TFirst, TSecond, TFirst>(storedProcedure,
                map: (first, second) =>
                {
                    var currentEntity = first;
                    Include<TFirst, TSecond>(currentEntity, second);
                    return currentEntity;
                });
        }
    }
    public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
        where TFirst : class where TSecond : class
    {
        using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            return await con.QueryAsync<TFirst, TSecond, TFirst>(storedProcedure,
                map: (first, second) =>
                {
                    var currentEntity = first;
                    Include<TFirst, TSecond>(currentEntity, second);
                    return currentEntity;
                },
                param: parameter,
                commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird>(string storedProcedure, string connectionId = "Default") where TFirst : class where TSecond : class where TThird : class
    {
        using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            return await con.QueryAsync<TFirst, TSecond, TThird, TFirst>(storedProcedure,
                map: (first, second, third) =>
                {
                    var currentEntity = first;
                    Include<TFirst, TSecond>(currentEntity, second);
                    Include<TFirst, TThird>(currentEntity, third);
                    return currentEntity;
                });
        }
    }
    public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class
    {
        using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            return await con.QueryAsync<TFirst, TSecond, TThird, TFirst>(storedProcedure,
                map: (first, second, third) =>
                {
                    var currentEntity = first;
                    Include<TFirst, TSecond>(currentEntity, second);
                    Include<TFirst, TThird>(currentEntity, third);
                    return currentEntity;
                },
                param: parameter,
                commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth>(string storedProcedure, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class where TFourth : class
    {
        using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            return await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFirst>(storedProcedure,
                map: (first, second, third, fourth) =>
                {
                    var currentEntity = first;
                    Include<TFirst, TSecond>(currentEntity, second);
                    Include<TFirst, TThird>(currentEntity, third);
                    Include<TFirst, TFourth>(currentEntity, fourth);
                    return currentEntity;
                });
        }
    }
    public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class where TFourth : class
    {
        using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            return await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFirst>(storedProcedure,
                map: (first, second, third, fourth) =>
                {
                    var currentEntity = first;
                    Include<TFirst, TSecond>(currentEntity, second);
                    Include<TFirst, TThird>(currentEntity, third);
                    Include<TFirst, TFourth>(currentEntity, fourth);
                    return currentEntity;
                },
                param: parameter,
                commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth>(string storedProcedure, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class
    {
        using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            return await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TFirst>(storedProcedure,
                map: (first, second, third, fourth, fifth) =>
                {
                    var currentEntity = first;
                    Include<TFirst, TSecond>(currentEntity, second);
                    Include<TFirst, TThird>(currentEntity, third);
                    Include<TFirst, TFourth>(currentEntity, fourth);
                    Include<TFirst, TFifth>(currentEntity, fifth);
                    return currentEntity;
                });
        }
    }
    public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class
    {
        using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            return await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TFirst>(storedProcedure,
                map: (first, second, third, fourth, fifth) =>
                {
                    var currentEntity = first;
                    Include<TFirst, TSecond>(currentEntity, second);
                    Include<TFirst, TThird>(currentEntity, third);
                    Include<TFirst, TFourth>(currentEntity, fourth);
                    Include<TFirst, TFifth>(currentEntity, fifth);
                    return currentEntity;
                },
                param: parameter,
                commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(string storedProcedure, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class
    {
        using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            return await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TFirst>(storedProcedure,
                map: (first, second, third, fourth, fifth, sixth) =>
                {
                    var currentEntity = first;
                    Include<TFirst, TSecond>(currentEntity, second);
                    Include<TFirst, TThird>(currentEntity, third);
                    Include<TFirst, TFourth>(currentEntity, fourth);
                    Include<TFirst, TFifth>(currentEntity, fifth);
                    Include<TFirst, TSixth>(currentEntity, sixth);
                    return currentEntity;
                });
        }
    }
    public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class
    {
        using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            return await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TFirst>(storedProcedure,
                map: (first, second, third, fourth, fifth, sixth) =>
                {
                    var currentEntity = first;
                    Include<TFirst, TSecond>(currentEntity, second);
                    Include<TFirst, TThird>(currentEntity, third);
                    Include<TFirst, TFourth>(currentEntity, fourth);
                    Include<TFirst, TFifth>(currentEntity, fifth);
                    Include<TFirst, TSixth>(currentEntity, sixth);
                    return currentEntity;
                },
                param: parameter,
                commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(string storedProcedure, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class where TSeventh : class
    {
        using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            return await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TFirst>(storedProcedure,
                map: (first, second, third, fourth, fifth, sixth, seventh) =>
                {
                    var currentEntity = first;
                    Include<TFirst, TSecond>(currentEntity, second);
                    Include<TFirst, TThird>(currentEntity, third);
                    Include<TFirst, TFourth>(currentEntity, fourth);
                    Include<TFirst, TFifth>(currentEntity, fifth);
                    Include<TFirst, TSixth>(currentEntity, sixth);
                    Include<TFirst, TSeventh>(currentEntity, seventh);
                    return currentEntity;
                });
        }
    }
    public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
       where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class where TSeventh : class
    {
        using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            return await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TFirst>(storedProcedure,
                map: (first, second, third, fourth, fifth, sixth, seventh) =>
                {
                    var currentEntity = first;
                    Include<TFirst, TSecond>(currentEntity, second);
                    Include<TFirst, TThird>(currentEntity, third);
                    Include<TFirst, TFourth>(currentEntity, fourth);
                    Include<TFirst, TFifth>(currentEntity, fifth);
                    Include<TFirst, TSixth>(currentEntity, sixth);
                    Include<TFirst, TSeventh>(currentEntity, seventh);
                    return currentEntity;
                },
                param: parameter,
                commandType: CommandType.StoredProcedure);
        }
    }

    private void Include<TFirst, TSecond>(TFirst baseEntity, TSecond includeEntity)
    {
        var tableName = typeof(TSecond).Name;
        var myType = typeof(TFirst);
        var pInfo = myType.GetProperty(tableName);
        if (pInfo != null)
            pInfo.SetValue(baseEntity, includeEntity, null);
    }
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
    public async Task<U?> SaveDataAndGetIdAsync<T, U>(
        string storedProcedure,
        T parameters,
        string connectionId = "Default")
    {
        using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
            SqlMapper.AddTypeHandler(new GuidHandler());
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
            var result = await connection.QueryAsync<U>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }
    }
}
