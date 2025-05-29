using Microsoft.Graph;
using OneDriveFileBackuper.Auth;

namespace OneDriveFileBackuper.GraphClient
{
    /// <summary>
    /// Default graph service client use for graph calls
    /// </summary>
    public class JwtGraphServiceClient : IJwtGraphServiceClient
    {
        private readonly IAuthJwtTokenProvider _authJwtTokenProvider;

        public JwtGraphServiceClient(IAuthJwtTokenProvider authJwtTokenProvider)
        {
            _authJwtTokenProvider = authJwtTokenProvider;
        }

        /// <summary>
        /// Create graph service client with custom token provider
        /// </summary>
        /// <returns></returns>
        public async Task<GraphServiceClient> CreateServiceClientAsync()
        {
            var token = await _authJwtTokenProvider.GenerateJwtTokenAsync();

            return new GraphServiceClient(new TokenProvider(token));
        }
    }
}
