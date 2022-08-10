namespace DataAccess.DbAccess
{
    public interface ISQLDataAccess
    {
        Task<IEnumerable<T>> LoadDataAsync<T>(string storedProcedure, string connectionId = "Default");
        Task<IEnumerable<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
        Task SaveDataAsync<T>(string storedProcedure, T parameters, string connectionId = "Default");
    }
}