using DataAccess.DbAccess;

namespace DataAccess.Repository;
public class FileRepository : IFileRepository
{
    private readonly ISQLDataAccess _db;
    public FileRepository(ISQLDataAccess db)
    {
        _db = db;
    }
    public async Task<string?> AddAsync(Models.File entity)
    {
        DateTime upate = DateTime.UtcNow;
        var result = await _db.SaveDataAndGetIdAsync<object, string>("dbo.spFile_Insert", new
        {
            FileId = entity.FileId,
            Extension = entity.Extension,
            FileIssueId = entity.FileIssueId,
            FileCommentId = entity.FileCommentId,
            Updated = upate
        });
        return result;
    }

    public async Task DeleteAsync(string fileId)
    {
        DateTime update = DateTime.UtcNow;
        await _db.SaveDataAsync("dbo.spFile_Delete", new { FileId = fileId, Updated = update });
    }

    public async Task<IEnumerable<Models.File>> GetByIssueIdAsync(int issueId)
    {
        var result = await _db.LoadDataAsync<Models.File, object>("spFile_GetByIssueId", new { IssueId = issueId });
        return result;
    }
    public async Task<IEnumerable<Models.File>> GetForCleanupAsync(TimeSpan timeSpan)
    {
        var updated = DateTime.UtcNow;
        updated = updated.AddMilliseconds(-timeSpan.TotalMilliseconds);
        var result = await _db.LoadDataAsync<Models.File, object>("spFile_GetForCleanup", new { Updated = updated });
        return result;
    }
}
