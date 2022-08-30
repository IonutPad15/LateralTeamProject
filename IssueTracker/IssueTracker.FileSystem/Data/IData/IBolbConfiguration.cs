using Microsoft.Extensions.Configuration;

namespace IssueTracker.FileSystem;
public interface IBolbConfiguration
{
    IBolbConfiguration Create(IConfiguration configuration);
}
