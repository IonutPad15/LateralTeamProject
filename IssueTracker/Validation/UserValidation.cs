using DataAccess.Models;
using Models.Info;

namespace Validation
{
    public static class UserValidation
    {
        public static bool isValid(UserRequest userRequest)
        {
            if(userRequest == null) return false;
            if (string.IsNullOrEmpty(userRequest.UserName) || string.IsNullOrEmpty(userRequest.Password)
                || string.IsNullOrEmpty(userRequest.Email)) return false;
            if (userRequest.UserName.Length > 50 || userRequest.Email.Length > 450)
                return false;
            return true;
        }
        public static bool isValid(User user)
        {
            if (user == null) return false;
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password)
                || string.IsNullOrEmpty(user.Email)) return false;
            if (user.UserName.Length > 50 || user.Email.Length > 450)
                return false;
            return true;
        }
    }
}
