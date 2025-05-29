using Microsoft.Graph;

namespace OneDriveFileBackuper.GraphClient
{
    /// <summary>
    /// Default graph service client use for graph calls
    /// </summary>
    public interface IJwtGraphServiceClient
    {
        /// <summary>
        /// Create graph service client with custom token provider
        /// </summary>
        /// <returns></returns>
        public Task<GraphServiceClient> CreateServiceClientAsync();
    }
}
