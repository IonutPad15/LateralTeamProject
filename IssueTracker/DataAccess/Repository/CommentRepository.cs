using System.Data.SqlClient;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ISQLDataAccess _db;
    public CommentRepository(ISQLDataAccess db)
    {
        _db = db;
    }
    public async Task<int> AddAsync(Comment comment)
    {
        try
        {
            var result = await _db.SaveDataAndGetIdAsync<object, int>("dbo.spComment_Insert",
                new
                {
                    comment.UserId,
                    comment.IssueId,
                    comment.CommentId,
                    comment.Author,
                    comment.Body,
                    comment.Created,
                    comment.Updated
                });
            return result;
        }
        catch (SqlException ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }
    public async Task<Comment?> GetByIdAsync(int id)
    {
        return (await _db.LoadCommentAsync<object>("spComment_Get", new { Id = id })).FirstOrDefault();
    }
    public async Task<IEnumerable<Comment>> GetAllByUserIdAsync(Guid id)
    {
        return (await _db.LoadCommentAsync<object>("dbo.spComment_GetAllByUserId", new { UserId = id }));
    }

    public async Task<IEnumerable<Comment?>> GetAllByIssueIdAsync(int id)
    {
        var comments = (await _db.LoadCommentAsync<object>("dbo.spComment_GetAllByIssueId", new { IssueId = id })).ToList();
        var comm = comments.Where(c => c.CommentId == null).ToList();
        for (int i = 0; i < comm.Count; ++i)
        {
            var replies = comments.Where(c => c.CommentId == comm[i].Id).ToList();
            comm[i].Replies = replies;
        }
        IEnumerable<Comment> result = comm;
        return result;
    }
    public async Task<IEnumerable<Comment?>> GetAllByCommentIdAsync(int id)
    {
        return (await _db.LoadCommentAsync<object>("dbo.spComment_GetAllByCommentId", new { CommentId = id }));
    }

    public async Task UpdateAsync(Comment comment)
    {
        try
        {
            await _db.SaveDataAsync("dbo.spComment_Update", new { comment.Id, comment.Body, comment.Updated });
        }
        catch (SqlException ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            await _db.SaveDataAsync("dbo.spComment_Delete", new { Id = id });
        }
        catch (SqlException ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }
}
public static class Extensions
{
    public static T[] RemoveAt<T>(this T[] source, int index)
    {
        var dest = new List<T>(source);
        dest.RemoveAt(index);
        return dest.ToArray();
    }
}
