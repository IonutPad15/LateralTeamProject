
namespace IssueTracker.FileSystem;
internal interface IBolbConfiguration : IConfigurationBase
{
    string Container { get; }
    string AccountName { get; }
    string AccountKey { get; }
}
internal interface IConfigurationBase
{
    string ConnectionString { get; }
}
internal interface IMetaDataConfiguration : IConfigurationBase
{
}
