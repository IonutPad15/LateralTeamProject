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
        var result = await _db.SaveDataAndGetIdAsync<dynamic, string>("dbo.spFile_Insert", new
        {
            FileId = entity.FileId,
            Extension = entity.Extension,
            FileIssueId = entity.FileIssueId,
            FileCommentId = entity.FileCommentId
        });
        return result;
    }

    public async Task DeleteAsync(string fileId)
    {
        await _db.SaveDataAsync("dbo.spFile_Delete", new { FileId = fileId });
    }

    public async Task<IEnumerable<Models.File>> GetByIssueIdAsync(int issueId)
    {
        var result = await _db.LoadDataAsync<Models.File, dynamic>("spFile_GetByIssueId", new { IssueId = issueId });
        return result;
    }
}
