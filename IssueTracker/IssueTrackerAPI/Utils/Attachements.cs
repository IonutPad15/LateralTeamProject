using IssueTracker.FileSystem;
using Models.Response;

namespace IssueTrackerAPI.Utils;

public class Attachements
{
    private readonly IFileProvider _fileProvider;
    public Attachements(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }
    public async Task<FileResponse> GetAttachements(DataAccess.Models.File? file)
    {
        var response = new FileResponse();
        if (file != null)
        {
            var fileModel = new IssueTracker.FileSystem.Models.File(file.FileId, file.Extension);
            List<IssueTracker.FileSystem.Models.File> files = new List<IssueTracker.FileSystem.Models.File>();
            files.Add(fileModel);
            var result = (await _fileProvider.GetAsync(files)).FirstOrDefault();
            if (result != null)
            {
                response.FileId = result.Id;
                response.Extension = result.Extension;
                response.IssueId = file.FileIssueId;
                response.CommentId = file.FileCommentId;
                response.Name = result.Name!;
                response.Type = result.Type;
                response.SizeKb = result.SizeKb;
                response.Link = result.Link!;
            }
        }
        return response;
    }
}
