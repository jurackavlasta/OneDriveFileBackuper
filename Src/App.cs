using Microsoft.Extensions.Logging;
using OneDriveFileBackuper.Handlers;

namespace OneDriveFileBackuper
{
    /// <summary>
    /// Main class which is execute
    /// </summary>
    public class App(IFileSyncHandler handler, ILogger<App> _logger)
    {
        /// <summary>
        /// Execute handler 
        /// </summary>
        /// <returns>Returns task</returns>
        public async Task RunAsync()
        {
            _logger.LogInformation("Start OneDriveFileDownloader");

            await handler.SyncAsync();

            _logger.LogInformation("Finish OneDriveFileDownloader");
        }
    }
}
