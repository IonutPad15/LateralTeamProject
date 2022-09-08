using DataAccess.Repository;
using IssueTracker.FileSystem;
namespace IssueTrackerAPI.Utils;

public class Cleanup : IHostedService
{
    private readonly IFileProvider _fileProvider;
    private readonly IFileRepository _fileData;
    private readonly IConfiguration _config;
    private static bool s_canceled;
    public Cleanup(IConfiguration config, IFileProvider fileProvider, IFileRepository fileRepository)
    {
        _config = config;
        _fileProvider = fileProvider;
        _fileData = fileRepository;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        CleanAsync(_config);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        return Task.CompletedTask;
    }

    public async Task CleanAsync(IConfiguration config)
    {
        while (s_canceled == false)
        {
            var timeSpanString = config.GetValue<string>("ConnectionStrings:TimeSpan");
            var timeSpan = TimeSpan.Parse(timeSpanString);
            int interval = (int)timeSpan.TotalMilliseconds;
            await Task.Delay(interval);
            var filesFromRepo = await _fileData.GetForCleanupAsync(timeSpan);
            foreach (var file in filesFromRepo)
            {
                var fileToDelete = new IssueTracker.FileSystem.Models.File(file.FileId, file.Extension);
                await _fileProvider.DeleteAsync(fileToDelete).ConfigureAwait(false);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        s_canceled = true;
        return Task.CompletedTask;
    }
}
