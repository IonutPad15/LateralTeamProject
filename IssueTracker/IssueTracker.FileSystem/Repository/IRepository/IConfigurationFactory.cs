namespace IssueTracker.FileSystem;
internal interface IConfigurationFactory
{
    IConfigurationBase Create<T>() where T : IConfigurationBase;
}
