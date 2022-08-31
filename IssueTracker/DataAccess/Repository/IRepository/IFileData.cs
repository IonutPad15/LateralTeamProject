using DataAccess.Models;

namespace DataAccess.Repository;
public interface IFileData
{
    Task<string> AddAsync(FileModel entity);
    Task DeleteAsync(string fileId);
}
