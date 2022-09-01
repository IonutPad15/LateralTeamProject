namespace DataAccess.Repository;
public interface IFileRepository
{
    Task<string?> AddAsync(Models.File entity);
    Task DeleteAsync(string fileId);
}
