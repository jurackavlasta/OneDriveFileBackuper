using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;

namespace OneDriveFileBackuper.Auth
{
    /// <summary>
    /// Token provider which add to header plain token
    /// </summary>
    public class TokenProvider : IAuthenticationProvider
    {
        private readonly string _accessToken;

        public TokenProvider(string accessToken)
        {
            _accessToken = accessToken;
        }

        /// <summary>
        /// Add token to header
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="additionalAuthenticationContext">Context</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns task</returns>
        public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
        {
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");

            return Task.CompletedTask;
        }
    }
}
