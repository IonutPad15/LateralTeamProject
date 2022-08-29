using Azure.Storage.Blobs;

namespace IssueTracker.FileSystem.Data.IData;
public interface IBolbData
{
    String Get(string name);
    void Upload(Stream file, string name);
}
