using Models.Request;

namespace Validation
{
    public class IssueValidation
    {
        public static bool IsValid(IssueRequest issue)
        {
            if(issue == null) return false;
            if(issue.Title == String.Empty || issue.Title!.Length > 50) return false;
            if (issue.Description == String.Empty) return false;
            if(issue.ProjectId == 0 
                || issue.IssueTypeId == 0 
                || issue.StatusId == 0 
                || issue.PriorityId == 0
                || issue.RoleId == 0) return false;
            return true;
        }
    }
}
