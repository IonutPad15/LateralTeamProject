using Microsoft.Extensions.Configuration;

namespace IssueTracker.FileSystem.Data.IData;
public interface IBolbConfiguration
{
    IBolbConfiguration Create(IConfiguration configuration);
}
