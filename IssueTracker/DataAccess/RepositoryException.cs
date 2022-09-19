namespace DataAccess;
public class RepositoryException : Exception
{
    public RepositoryException(string message)
        : base($"Error message: {message} ")
    {
    }
}
