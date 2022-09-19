namespace IssueTracker.FileSystem;
public class FileSystemException : Exception
{
    public FileSystemException(string message)
        : base($"Error message: {message} ")
    {
    }
}
