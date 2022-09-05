namespace IssueTracker.FileSystem;
internal interface IBolbData
{
    Task<IEnumerable<Models.FileModel>> GetFilesAsync(IEnumerable<Models.FileModel> files);
    Task UploadFileAsync(Models.FileModel file);
    Task<Stream> DownloadFileAsync(string link);
}
