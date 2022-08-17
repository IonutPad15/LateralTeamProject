using DataAccess.Data.IData;
using DataAccess.DbAccess;
using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Data
{
    public class CommentData : ICommentData
    {
        private readonly ISQLDataAccess _db;
        public CommentData(ISQLDataAccess db)
        {
            _db = db;
        }
        public async Task AddAsync(Comment comment)
        {
            await _db.SaveDataAsync("dbo.spComment_Insert",
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
        }
        public async Task<Comment?> GetByIdAsync(int id)
        {
            return (await _db.LoadDataAsync<Comment, object>("dbo.spComment_Get", new { Id = id })).FirstOrDefault();
        }
        public async Task<IEnumerable<Comment>> GetAllByUserIdAsync(Guid id)
        {
            return (await _db.LoadDataAsync<Comment, object>("dbo.spComment_GetAllByUserId", new { UserId = id }));
        }
        
        public async Task<IEnumerable<Comment?>> GetAllByIssueIdAsync(int id)
        {
            var comments = (await _db.LoadDataAsync<Comment, object>("dbo.spComment_GetAllByIssueId", new { IssueId = id })).ToArray();
            int n = comments.Count();
            for(int i = 0; i < n ; ++i)
            { 
                if (comments[i].IssueId == null)
                {
                    var commToReply = comments.Where(c=>c.Id == comments[i].CommentId).FirstOrDefault();
                    int index = Array.IndexOf(comments, commToReply);
                    comments[index].Replies.Add(comments[i]);
                    comments = Extensions.RemoveAt(comments, i);
                    --i;
                    --n;
                }
            }
            IEnumerable<Comment> result = comments;
            return result;
        }
        public async Task<IEnumerable<Comment?>> GetAllByCommentIdAsync(int id)
        {
            return (await _db.LoadDataAsync<Comment, object>("dbo.spComment_GetAllByCommentId", new { CommentId = id }));
        }

        public async Task UpdateAsync(Comment comment)
        {
            await _db.SaveDataAsync("dbo.spComment_Update", new { comment.Id, comment.Body, comment.Updated });
        }

        public async Task DeleteAsync(int id)
        {
            await _db.SaveDataAsync("dbo.spComment_Delete", new { Id = id });
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
}
