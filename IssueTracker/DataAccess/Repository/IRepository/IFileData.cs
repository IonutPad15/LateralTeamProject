using DataAccess.Models;

namespace DataAccess.Repository;
public interface IFileData
{
    Task<int> AddAsync(FileModel entity);
    Task DeleteAsync(string fileId);
}
