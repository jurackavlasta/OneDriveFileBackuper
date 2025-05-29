using Microsoft.Extensions.Options;
using Microsoft.Graph.Models;
using OneDriveFileBackuper.GraphClient;
using OneDrivePhotoDownloader.Options;

namespace OneDriveFileBackuper.OneDrive
{
    /// <summary>
    /// Image service which use as source OneDrive
    /// </summary>
    public class OneDriveService(IOptions<OneDriveOption> _oneDriveOption, IJwtGraphServiceClient _defaultGraphServiceClient) : IOneDriveClient
    {
        /// <summary>
        /// Load files from one drive with default graph service client 
        /// </summary>
        /// <returns>Returns files from one drive</returns>
        public async Task<IEnumerable<DriveItem>> LoadFilesAsync()
        {
            var client = await _defaultGraphServiceClient.CreateServiceClientAsync();

            var images = (await client.Drives[_oneDriveOption.Value.Drive].Items[_oneDriveOption.Value.FilesFolder].Children.GetAsync())?.Value;

            if (images == null || !images.Any())
                return new List<DriveItem>();

            return images.Where(x => x.File != null);
        }

        /// <summary>
        /// Download file from one drive and returns task of bytes array.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Returns task of bytes array</returns>
        public async Task<byte[]> DownloadFileAsync(DriveItem file)
        {
            var downloadUrl = file.AdditionalData["@microsoft.graph.downloadUrl"]!.ToString();

            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetByteArrayAsync(downloadUrl);
            }
        }

        /// <summary>
        /// Delete file from one drive by id
        /// </summary>
        /// <param name="file">File in onedrive</param>
        /// <returns>Returns task</returns>
        public async Task DeleteFileAsync(DriveItem file)
        {
            var client = await _defaultGraphServiceClient.CreateServiceClientAsync();

            await client.Drives[_oneDriveOption.Value.Drive].Items[file.Id].DeleteAsync();
        }
    }
}
