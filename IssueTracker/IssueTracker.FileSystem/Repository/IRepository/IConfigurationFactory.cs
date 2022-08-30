namespace IssueTracker.FileSystem;
public interface IConfigurationFactory
{
    IConfigurationBase Create<T>() where T : IConfigurationBase;
}
