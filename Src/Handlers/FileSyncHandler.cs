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
        /// <summary>
        /// Sync file from onedrive and backup
        /// </summary>
        /// <returns>Returns task</returns>
        public async Task SyncAsync()
        {
            var files = await _oneDriveService.LoadFilesAsync();

            var tasks = files.Select(async file =>
            {
                var result = await _oneDriveService.DownloadFileAsync(file);
                await _fileStorage.SaveFileAsync(file.Name ?? Guid.NewGuid().ToString(), result);

                if (_backupSettingsOption.Value.DeleteAfterBackup)
                {
                    await _oneDriveService.DeleteFileAsync(file);
                }

                _logger.LogInformation($"The {file.Name} file has been backed up and deleted.");
            });

            await Task.WhenAll(tasks);

            _logger.LogInformation("All files were successfully backed up and deleted.");
        }
    }
}
