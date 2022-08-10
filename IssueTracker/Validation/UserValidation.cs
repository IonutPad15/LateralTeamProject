
using Models.Request;

namespace Validation
{
    public static class UserValidation
    {
        public static bool IsValid(UserRequest userRequest)
        {
            if(userRequest == null) return false;
            if (string.IsNullOrEmpty(userRequest.UserName) || string.IsNullOrEmpty(userRequest.Password)
                || string.IsNullOrEmpty(userRequest.Email)) return false;
            if (userRequest.UserName.Length > 50 || userRequest.Email.Length > 450)
                return false;
            return true;
        }
        
    }
}
