using Microsoft.Extensions.Options;
using OneDriveFileBackuper.Options;

namespace OneDriveFileBackuper.Storages
{
    /// <summary>
    /// Work with storage
    /// </summary>
    public class LocalFileStorage(IOptions<LocalStorageOption> backupStorageOption) : IFileStorage
    {
        /// <summary>
        /// Save file by name and path in configuration
        /// </summary>
        /// <param name="name">Name of file</param>
        /// <param name="file">Content of file</param>
        /// <returns>Returns task</returns>
        public Task SaveFileAsync(string name, byte[] file)
        {
            return File.WriteAllBytesAsync(Path.Combine(backupStorageOption.Value.Path!, name), file);
        }
    }
}
