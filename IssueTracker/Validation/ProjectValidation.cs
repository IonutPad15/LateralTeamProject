using Models.Request;

namespace Validation
{
    public class ProjectValidation
    {
        public static bool IsValid(ProjectRequest project)
        {
            if(project == null) return false;
            if(project.Title == String.Empty || project.Title!.Length > 50) return false;
            if(project.Description == String.Empty) return false;
            return true;
        }
    }
}
