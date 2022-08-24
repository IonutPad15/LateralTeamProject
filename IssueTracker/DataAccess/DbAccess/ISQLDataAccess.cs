#pragma warning disable IDE0073 // The file header is missing or not located at the top of the file
using DataAccess.Models;
#pragma warning restore IDE0073 // The file header is missing or not located at the top of the file

namespace DataAccess.DbAccess;

public interface ISQLDataAccess
{
    Task<IEnumerable<T>> LoadDataAsync<T>(string storedProcedure, string connectionId = "Default");
    Task<IEnumerable<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
    Task<IEnumerable<Participant>> GetByProjectIdAsync(string storedProcedure, int id, string connectionId = "Default");
    Task SaveDataAsync<T>(string storedProcedure, T parameters, string connectionId = "Default");
    Task<U?> SaveDataAndGetIdAsync<T, U>(string storedProcedure, T parameters, string connectionId = "Default");
    Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond>(string storedProcedure, string connectionId = "Default")
        where TFirst : class where TSecond : class;
    Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
        where TFirst : class where TSecond : class;
    Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird>(string storedProcedure, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class;
    Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class;
    Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth>(string storedProcedure, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class where TFourth : class;
    Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class where TFourth : class;
    Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth>(string storedProcedure, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class;
    Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class;
    Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(string storedProcedure, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class;
    Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class;
    Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(string storedProcedure, string connectionId = "Default")
        where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class where TSeventh : class;
    Task<IEnumerable<TFirst>> LoadDataAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TParameter>(string storedProcedure, TParameter parameter, string connectionId = "Default")
       where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class where TSeventh : class;
}
