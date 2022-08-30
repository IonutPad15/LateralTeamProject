using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Repository;
public class FileData: IFileData
{
    private readonly ISQLDataAccess _db;
    public FileData(ISQLDataAccess db)
    {
        _db = db;
    }
    public async Task<int> AddAsync(FileModel entity)
    {
        var result = await _db.SaveDataAndGetIdAsync<dynamic, int>("dbo.spFile_Insert", new
        {
            FileId = entity.FileId,
            IssueId = entity.IssueId,
            CommentId = entity.CommentId
        });
        return result;
    }

    public async Task DeleteAsync(string fileId)
    {
        await _db.SaveDataAsync("dbo.spFile_Delete", new { FileId = fileId });
    }
}
