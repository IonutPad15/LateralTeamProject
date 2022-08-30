
namespace IssueTracker.FileSystem;
public interface IBolbConfiguration : IConfigurationBase
{
    string Container { get; }
}
public interface IConfigurationBase
{
    string ConnectionString { get; }
}
public interface IMetaDataConfiguration : IConfigurationBase
{
}
