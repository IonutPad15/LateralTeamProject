using System.Data.SqlClient;
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
        try
        {
            DateTime update = DateTime.UtcNow;
            var result = await _db.SaveDataAndGetIdAsync<object, string>("dbo.spFile_Insert", new
            {
                FileId = entity.FileId,
                Extension = entity.Extension,
                FileIssueId = entity.FileIssueId,
                FileCommentId = entity.FileCommentId,
                Updated = update,
                FileUserId = entity.FileUserId
            });
            return result;
        }
        catch (SqlException ex)
        {
            throw new RepositoryException(ex.Message);
        }
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
    public async Task<Models.File?> GetAsync(string fileId)
    {
        var result = (await _db.LoadDataAsync<Models.File, object>("spFile_GetByFileId", new { FileId = fileId })).FirstOrDefault();
        return result;
    }
    public async Task<IEnumerable<Models.File>> GetForCleanupAsync(TimeSpan timeSpan)
    {
        var updated = DateTime.UtcNow;
        updated = updated.AddMilliseconds(-timeSpan.TotalMilliseconds);
        var result = await _db.LoadDataAsync<Models.File, object>("spFile_GetForCleanup", new { Updated = updated });
        return result;
    }
    public async Task<IEnumerable<Models.File>> GetForCleanupAsync()
    {
        var result = await _db.LoadDataAsync<Models.File, dynamic>("spFile_GetForCleanup", new { Updated = DateTime.UtcNow });
        return result;
    }
}
