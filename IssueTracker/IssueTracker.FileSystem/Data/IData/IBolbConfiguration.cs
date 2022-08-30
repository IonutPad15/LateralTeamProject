using Microsoft.Extensions.Configuration;

namespace FileSystem.Data.IData;
public interface IBolbConfiguration
{
    IBolbConfiguration Create(IConfiguration configuration);
}
