using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneDriveFileBackuper.OneDrive;
using OneDriveFileBackuper.Options;
using OneDriveFileBackuper.Storages;

namespace OneDriveFileBackuper.Handlers
{
    /// <summary>
    /// Handler for file sync from onedrive and save
    /// </summary>
    public class FileSyncHandler(IOptions<BackupSettingsOption> _backupSettingsOption, IOneDriveClient _oneDriveService, IFileStorage _fileStorage, ILogger<FileSyncHandler> _logger) : IFileSyncHandler
    {
        private const int MaxParallel = 4;

        /// <summary>
        /// Sync file from onedrive and backup
        /// </summary>
        /// <returns>Returns task</returns>
        public async Task SyncAsync()
        {
            int size = 1;

            var throttler = new SemaphoreSlim(MaxParallel);

            var files = await _oneDriveService.ProcessFilesAsync(async file =>
            {
                await throttler.WaitAsync();
                try
                {
                    var result = await _oneDriveService.DownloadFileAsync(file);

                    _logger.LogInformation($"The {file.Name} file downloaded.");

                    await _fileStorage.SaveFileAsync(file.Name ?? Guid.NewGuid().ToString(), result);

                    _logger.LogInformation($"The {file.Name} file saved.");

                    if (_backupSettingsOption.Value.DeleteAfterBackup)
                    {
                        await _oneDriveService.DeleteFileAsync(file);
                        _logger.LogInformation($"The {file.Name} file deleted.");
                    }

                    _logger.LogInformation($"The {file.Name} file number {size} done.");

                    size++;
                }
                finally
                {
                    throttler.Release();
                }
            });

            await Task.WhenAll(files);

            _logger.LogInformation("All files were successfully backed up and deleted.");
        }
    }
}
