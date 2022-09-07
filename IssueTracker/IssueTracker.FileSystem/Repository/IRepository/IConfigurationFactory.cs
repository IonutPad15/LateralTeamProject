namespace IssueTracker.FileSystem;
internal interface IConfigurationFactory
{
    T Create<T>() where T : IConfigurationBase;
}
