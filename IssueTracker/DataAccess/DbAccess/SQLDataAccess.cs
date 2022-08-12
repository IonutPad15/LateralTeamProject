using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using DataAccess.Utils;
using System.Reflection;

namespace DataAccess.DbAccess
{
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
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
        } //TODO: de gandit putin la aceste metode!
        public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond>(string storedProcedure, string connectionId = "Default")
            where TFirst : class where TSecond : class
        {
            var dictionary = new Dictionary<string, TFirst>();
            using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                return await con.QueryAsync<TFirst, TSecond, TFirst>(storedProcedure,
                    map: (first, second) =>
                    {
                        var nameProrerty = typeof(TFirst).GetProperty("Id");
                        var oneId = (string)nameProrerty?.GetValue(first);
                        if (!dictionary.TryGetValue(oneId, out var currentOne))
                        {
                            currentOne = first;
                            dictionary.Add(oneId, currentOne);
                        }
                        var secondTableName = typeof(TSecond).Name;
                        Type myType = typeof(TFirst);
                        PropertyInfo pInfo = myType.GetProperty(secondTableName);
                        pInfo.SetValue(currentOne, second, null);
                        return currentOne;
                    });
            }
        }
        public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default") 
            where TFirst: class where TSecond : class
        {
            var dictionary = new Dictionary<string, TFirst>();
            using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                return await con.QueryAsync<TFirst, TSecond, TFirst>(storedProcedure,
                    map: (first, second) =>
                    {
                        var nameProrerty = typeof(TFirst).GetProperty("Id");
                        var oneId = (string)nameProrerty?.GetValue(first);
                        if (!dictionary.TryGetValue(oneId, out var currentOne))
                        {
                            currentOne = first;
                            dictionary.Add(oneId, currentOne);
                        }
                        var secondTableName = typeof(TSecond).Name;
                        Type myType = typeof(TFirst);
                        PropertyInfo pInfo = myType.GetProperty(secondTableName);
                        pInfo.SetValue(currentOne, second, null);
                        return currentOne;
                    },
                    param: parameter,
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird>(string storedProcedure, string connectionId = "Default") where TFirst : class where TSecond : class where TThird : class
        {
            var dictionary = new Dictionary<string, TFirst>();
            var listObject = new List<TFirst>();
            using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                await con.QueryAsync<TFirst, TSecond, TThird, TFirst>(storedProcedure,
                    map: (first, second, third) =>
                    {
                        var nameProrerty = typeof(TFirst).GetProperty("Id");
                        var oneId = nameProrerty?.GetValue(first).ToString();
                        if (!dictionary.TryGetValue(oneId, out var currentOne))
                        {
                            currentOne = first;
                            dictionary.Add(oneId, currentOne);
                        }
                        var secondTableName = typeof(TSecond).Name;
                        Type myType = typeof(TFirst);
                        PropertyInfo pInfo = myType.GetProperty(secondTableName);
                        pInfo.SetValue(currentOne, second, null);

                        var thirdTableName = typeof(TThird).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(thirdTableName);
                        pInfo.SetValue(currentOne, third, null);
                        return currentOne;
                    });
            }
            foreach (var item in dictionary)
            {
                listObject.Add(item.Value);
            }

            return listObject;
        }
        public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default") 
            where TFirst : class where TSecond : class where TThird : class
        {
            var dictionary = new Dictionary<string, TFirst>();
            using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                return await con.QueryAsync<TFirst, TSecond, TThird, TFirst>(storedProcedure,
                    map: (first, second, third) =>
                    {
                        var nameProrerty = typeof(TFirst).GetProperty("Id");
                        var oneId = nameProrerty?.GetValue(first).ToString();
                        if (!dictionary.TryGetValue(oneId, out var currentOne))
                        {
                            currentOne = first;
                            dictionary.Add(oneId, currentOne);
                        }
                        var secondTableName = typeof(TSecond).Name;
                        Type myType = typeof(TFirst);
                        PropertyInfo pInfo = myType.GetProperty(secondTableName);
                        pInfo.SetValue(currentOne, second, null);

                        var thirdTableName = typeof(TThird).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(thirdTableName);
                        pInfo.SetValue(currentOne, third, null);
                        return currentOne;
                    },
                    param: parameter,
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth>(string storedProcedure, string connectionId = "Default") 
            where TFirst : class where TSecond : class where TThird : class where TFourth : class
        {
            var dictionary = new Dictionary<string, TFirst>();
            var listObject = new List<TFirst>();
            using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFirst>(storedProcedure,
                    map: (first, second, third, fourth) =>
                    {
                        var nameProrerty = typeof(TFirst).GetProperty("Id");
                        var oneId = nameProrerty?.GetValue(first).ToString();
                        if (!dictionary.TryGetValue(oneId, out var currentOne))
                        {
                            currentOne = first;
                            dictionary.Add(oneId, currentOne);
                        }
                        var tableName = typeof(TSecond).Name;
                        Type myType = typeof(TFirst);
                        PropertyInfo pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, second, null);

                        tableName = typeof(TThird).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, third, null);

                        tableName = typeof(TFourth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, fourth, null);
                        return currentOne;
                    });
            }
            foreach (var item in dictionary)
            {
                listObject.Add(item.Value);
            }

            return listObject;
        }
        public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
            where TFirst : class where TSecond : class where TThird : class where TFourth : class
        {
            var dictionary = new Dictionary<string, TFirst>();
            using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                return await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFirst>(storedProcedure,
                    map: (first, second, third, fourth) =>
                    {
                        var nameProrerty = typeof(TFirst).GetProperty("Id");
                        var oneId = nameProrerty?.GetValue(first).ToString();
                        if (!dictionary.TryGetValue(oneId, out var currentOne))
                        {
                            currentOne = first;
                            dictionary.Add(oneId, currentOne);
                        }
                        var tableName = typeof(TSecond).Name;
                        Type myType = typeof(TFirst);
                        PropertyInfo pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, second, null);

                        tableName = typeof(TThird).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, third, null);

                        tableName = typeof(TFourth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, fourth, null);
                        return currentOne;
                    },
                    param: parameter,
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth>(string storedProcedure, string connectionId = "Default")
            where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class
        {
            var dictionary = new Dictionary<string, TFirst>();
            var listObject = new List<TFirst>();
            using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TFirst>(storedProcedure,
                    map: (first, second, third, fourth, fifth) =>
                    {
                        var nameProrerty = typeof(TFirst).GetProperty("Id");
                        var oneId = nameProrerty?.GetValue(first).ToString();
                        if (!dictionary.TryGetValue(oneId, out var currentOne))
                        {
                            currentOne = first;
                            dictionary.Add(oneId, currentOne);
                        }
                        var tableName = typeof(TSecond).Name;
                        Type myType = typeof(TFirst);
                        PropertyInfo pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, second, null);

                        tableName = typeof(TThird).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, third, null);

                        tableName = typeof(TFourth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, fourth, null);

                        tableName = typeof(TFifth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, fifth, null);
                        return currentOne;
                    });
            }
            foreach (var item in dictionary)
            {
                listObject.Add(item.Value);
            }

            return listObject;
        }
        public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
            where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class
        {
            var dictionary = new Dictionary<string, TFirst>();
            using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                return await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TFirst>(storedProcedure,
                    map: (first, second, third, fourth, fifth) =>
                    {
                        var nameProrerty = typeof(TFirst).GetProperty("Id");
                        var oneId = nameProrerty?.GetValue(first).ToString();
                        if (!dictionary.TryGetValue(oneId, out var currentOne))
                        {
                            currentOne = first;
                            dictionary.Add(oneId, currentOne);
                        }
                        var tableName = typeof(TSecond).Name;
                        Type myType = typeof(TFirst);
                        PropertyInfo pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, second, null);

                        tableName = typeof(TThird).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, third, null);

                        tableName = typeof(TFourth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, fourth, null);

                        tableName = typeof(TFifth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, fifth, null);
                        return currentOne;
                    },
                    param: parameter,
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(string storedProcedure, string connectionId = "Default")
            where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class
        {
            var dictionary = new Dictionary<string, TFirst>();
            using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                return await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TFirst>(storedProcedure,
                    map: (first, second, third, fourth, fifth, sixth) =>
                    {
                        var nameProrerty = typeof(TFirst).GetProperty("Id");
                        var oneId = nameProrerty?.GetValue(first).ToString();
                        if (!dictionary.TryGetValue(oneId, out var currentOne))
                        {
                            currentOne = first;
                            dictionary.Add(oneId, currentOne);
                        }
                        var tableName = typeof(TSecond).Name;
                        Type myType = typeof(TFirst);
                        PropertyInfo pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, second, null);

                        tableName = typeof(TThird).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, third, null);

                        tableName = typeof(TFourth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, fourth, null);

                        tableName = typeof(TFifth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, fifth, null);

                        tableName = typeof(TSixth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, sixth, null);
                        return currentOne;
                    });
            }
        }
        public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
            where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class
        {
            var dictionary = new Dictionary<string, TFirst>();
            using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                return await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TFirst>(storedProcedure,
                    map: (first, second, third, fourth, fifth, sixth) =>
                    {
                        var nameProrerty = typeof(TFirst).GetProperty("Id");
                        var oneId = nameProrerty?.GetValue(first).ToString();
                        if (!dictionary.TryGetValue(oneId, out var currentOne))
                        {
                            currentOne = first;
                            dictionary.Add(oneId, currentOne);
                        }
                        var tableName = typeof(TSecond).Name;
                        Type myType = typeof(TFirst);
                        PropertyInfo pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, second, null);

                        tableName = typeof(TThird).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, third, null);

                        tableName = typeof(TFourth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, fourth, null);

                        tableName = typeof(TFifth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, fifth, null);

                        tableName = typeof(TSixth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, sixth, null);
                        return currentOne;
                    },
                    param: parameter,
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(string storedProcedure, string connectionId = "Default")
            where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class where TSeventh : class
        {
            var dictionary = new Dictionary<string, TFirst>();
            var listObject = new List<TFirst>();
            using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TFirst>(storedProcedure,
                    map: (first, second, third, fourth, fifth, sixth, seventh) =>
                    {
                        var nameProrerty = typeof(TFirst).GetProperty("Id");
                        var oneId = nameProrerty?.GetValue(first).ToString();
                        if (!dictionary.TryGetValue(oneId, out var currentOne))
                        {
                            currentOne = first;
                            dictionary.Add(oneId, currentOne);
                        }
                        var tableName = typeof(TSecond).Name;
                        Type myType = typeof(TFirst);
                        PropertyInfo pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, second, null);

                        tableName = typeof(TThird).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, third, null);

                        tableName = typeof(TFourth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, fourth, null);

                        tableName = typeof(TFifth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, fifth, null);

                        tableName = typeof(TSixth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, sixth, null);
                        
                        tableName = typeof(TSeventh).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, seventh, null);
                        return currentOne;
                    });
            }
            foreach (var item in dictionary)
            {
                listObject.Add(item.Value);
            }

            return listObject;
        }
        public async Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
           where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class where TSeventh : class
        {
            var dictionary = new Dictionary<string, TFirst>();
            using (var con = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                return await con.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TFirst>(storedProcedure,
                    map: (first, second, third, fourth, fifth, sixth, seventh) =>
                    {
                        var nameProrerty = typeof(TFirst).GetProperty("Id");
                        var oneId = nameProrerty?.GetValue(first).ToString();
                        if (!dictionary.TryGetValue(oneId, out var currentOne))
                        {
                            currentOne = first;
                            dictionary.Add(oneId, currentOne);
                        }
                        var tableName = typeof(TSecond).Name;
                        Type myType = typeof(TFirst);
                        PropertyInfo pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, second, null);

                        tableName = typeof(TThird).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, third, null);

                        tableName = typeof(TFourth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, fourth, null);

                        tableName = typeof(TFifth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, fifth, null);

                        tableName = typeof(TSixth).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, sixth, null);

                        tableName = typeof(TSeventh).Name;
                        myType = typeof(TFirst);
                        pInfo = myType.GetProperty(tableName);
                        pInfo.SetValue(currentOne, seventh, null);
                        return currentOne;
                    },
                    param: parameter,
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
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
}
