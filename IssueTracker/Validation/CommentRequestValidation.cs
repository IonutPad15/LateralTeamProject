using Models.Request;

namespace Validation
{
    public class CommentRequestValidation
    {
        public static bool IsValid(CommentRequest commentRequest)
        {
            if (commentRequest == null) return false;
            if(commentRequest.IssueId == null && commentRequest.CommentId == null) return false;
            if (commentRequest.IssueId <=0) return false;
            if (commentRequest.CommentId <= 0) return false;
            if (string.IsNullOrEmpty(commentRequest.Body) ) return false;
            if(commentRequest.Body.Length > 450) return false;
            return true;
        }
    }
}
