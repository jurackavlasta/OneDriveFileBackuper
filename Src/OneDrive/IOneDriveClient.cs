using Microsoft.Graph.Models;

namespace OneDriveFileBackuper.OneDrive
{
    /// <summary>
    /// Image service which use as source OneDrive
    /// </summary>
    public interface IOneDriveClient
    {
        /// <summary>
        /// Load files from one drive with default graph service client 
        /// </summary>
        /// <returns>Returns files from one drive</returns>
        public Task<IEnumerable<DriveItem>> LoadFilesAsync();

        /// <summary>
        /// Download file from one drive and returns task of bytes array.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Returns task of bytes array</returns>
        public Task<byte[]> DownloadFileAsync(DriveItem file);

        /// <summary>
        /// Delete file from one drive by id
        /// </summary>
        /// <param name="file">File in onedrive</param>
        /// <returns>Returns task</returns>
        public Task DeleteFileAsync(DriveItem file);
    }
}
