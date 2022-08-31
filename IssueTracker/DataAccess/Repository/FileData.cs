using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Repository;
public class FileData : IFileData
{
    private readonly ISQLDataAccess _db;
    public FileData(ISQLDataAccess db)
    {
        _db = db;
    }
    public async Task<string> AddAsync(FileModel entity)
    {
        var result = await _db.SaveDataAndGetIdAsync<dynamic, string>("dbo.spFile_Insert", new
        {
            FileId = entity.FileId,
            GroupId = entity.GroupId,
            FileIssueId = entity.FileIssueId,
            FileCommentId = entity.FileCommentId
        });
        return result;
    }

    public async Task DeleteAsync(string fileId)
    {
        await _db.SaveDataAsync("dbo.spFile_Delete", new { FileId = fileId });
    }
}
