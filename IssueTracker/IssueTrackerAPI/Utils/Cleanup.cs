using DataAccess.Repository;
using IssueTracker.FileSystem;
namespace IssueTrackerAPI.Utils;

public class Cleanup : IHostedService
{
    private readonly IFileProvider _fileProvider;
    private readonly IFileRepository _fileData;
    public Cleanup(IConfiguration config, IFileProvider fileProvider, IFileRepository fileRepository)
    {
        _fileProvider = fileProvider;
        _fileData = fileRepository;
        var daysString = config.GetValue<string>("ConnectionStrings:CleanupDaysInterval");
        int days = int.Parse(daysString);
        int Interval = (1000 * 60 * 60 * 24 * days);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task StartClean(int interval)
    {
        while (true)
        {
            await Task.Delay(interval);
            var filesFromRepo = await _fileData.GetForCleanupAsync();
            foreach (var file in filesFromRepo)
            {
                var fileToDelete = new IssueTracker.FileSystem.Models.File(file.FileId, file.Extension);
                await _fileProvider.DeleteAsync(fileToDelete);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
