using Azure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using OneDriveFileBackuper.GraphClients;
using OneDriveFileBackuper.Options;

namespace OneDriveFileBackuper.OneDrive
{
    /// <summary>
    /// Image service which use as source OneDrive
    /// </summary>
    public class OneDriveService(IOptions<OneDriveOption> _oneDriveOption, IJwtGraphServiceClient _defaultGraphServiceClient, ILogger<OneDriveService> _logger, IHttpClientFactory _httpClientFactory) : IOneDriveClient
    {
        private readonly string _ondeDriveFileDownloadPropertyName = "@microsoft.graph.downloadUrl";
        private readonly int _maxPageSize = 100;

        /// <summary>
        /// Load files from one drive with default graph service client 
        /// </summary>
        /// <returns>Returns files from one drive</returns>
        public async Task<IEnumerable<Task>> ProcessFilesAsync(Func<DriveItem, Task> onFileDownload)
        {
            var client = await _defaultGraphServiceClient.CreateServiceClientAsync();

            var downloadFiles = (await client
                .Drives[_oneDriveOption.Value.Drive]
                .Items[_oneDriveOption.Value.FilesFolder]
                .Children
                .GetAsync(x =>
                {
                    x.QueryParameters.Top = _maxPageSize;
                }));

            var tasks = new List<Task>();

            if (downloadFiles == null)
                return tasks;

            var pageIterator = PageIterator<DriveItem, DriveItemCollectionResponse>
                .CreatePageIterator(
                client, 
                downloadFiles, 
                (file) =>
                {
                    if (file.File != null)
                    {
                        tasks.Add(onFileDownload(file));
                    }

                    return true;
                });

            await pageIterator.IterateAsync();

            return tasks;
        }

        /// <summary>
        /// Download file from one drive and returns task of bytes array.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Returns task of bytes array</returns>
        public async Task<byte[]> DownloadFileAsync(DriveItem file)
        {
            var downloadUrl = file.AdditionalData[_ondeDriveFileDownloadPropertyName]!.ToString();

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(10);

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
