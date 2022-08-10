using Models.Request;

namespace Validation
{
    public class CredentialsValidation
    {
        public static bool IsValid(Credentials credentials)
        {
            if (credentials == null) return false;
            if (string.IsNullOrEmpty(credentials.NameEmail) || string.IsNullOrEmpty(credentials.Password))
                return false;
            if(credentials.NameEmail.Length >450) return false;
            return true;
        }
    }
}
