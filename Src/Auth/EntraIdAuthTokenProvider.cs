using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;
using OneDrivePhotoDownloader.Options;

namespace OneDriveFileBackuper.Auth
{
    /// <summary>
    /// Token manager to acquire token
    /// </summary>
    public class EntraIdAuthTokenProvider : IAuthJwtTokenProvider
    {
        private readonly ILogger<EntraIdAuthTokenProvider> _logger;
        private readonly EntraIdOption _options;
        private IPublicClientApplication? _pca;
        private string _cacheName = "msal_cache.bin";
        private string _cachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "OneDrivePhotoDownloader");
        private string[] _scopes = new[] { "Files.ReadWrite", "Files.Read", "offline_access" };
        private StorageCreationProperties _storageProperties;
        private bool _initialized = false;

        /// <summary>
        /// JWT generated token
        /// </summary>
        public string? AccessToken { get; set; }

        public EntraIdAuthTokenProvider(ILogger<EntraIdAuthTokenProvider> logger, IOptions<EntraIdOption> options)
        {
            _logger = logger;
            _options = options.Value;

            _storageProperties = new StorageCreationPropertiesBuilder(_cacheName, _cachePath).Build();
        }

        /// <summary>
        /// Generate JWT Access token 
        /// </summary>
        /// <returns>Return JWT token</returns>
        public async Task<string> GenerateJwtTokenAsync()
        {
            if (AccessToken == null) {
                AccessToken = (await GenerateTokenAsync()).AccessToken;
            }

            return AccessToken;
        }

        /// <summary>
        /// Generate token - try select silent token or generate new by device code
        /// </summary>
        /// <returns></returns>
        public async Task<AuthenticationResult> GenerateTokenAsync()
        {
            await EnsureInitializedAsync();

            return await AcquireTokenAsync(_pca!);
        }

        /// <summary>
        /// Check that the initial setup of the client and cache has been done.
        /// </summary>
        /// <returns>Retruns task</returns>
        private async Task EnsureInitializedAsync()
        {
            if (_initialized) return;

            _pca = PublicClientApplicationBuilder
                .Create(_options.ClientId)
                .WithDefaultRedirectUri()
                .Build();

            var cacheHelper = await MsalCacheHelper.CreateAsync(_storageProperties);
            cacheHelper.RegisterCache(_pca.UserTokenCache);

            _initialized = true;
        }

        /// <summary>
        /// Getting an access token
        /// </summary>
        /// <param name="pca">Public client app</param>
        /// <param name="account">The account under which the token is stored</param>
        /// <returns>Returns authentication result</returns>
        public async Task<AuthenticationResult> AcquireTokenAsync(IPublicClientApplication pca)
        {
            _logger.LogInformation("Acquire token - start");

            try
            {
                return await AcquireTokenSilentAsync(pca);
            }
            catch (MsalUiRequiredException)
            {
                return await AcquireTokenWithDeviceCodeAsync(pca);
            }
        }

        /// <summary>
        /// It will attempt to acquire the silent token from storage.
        /// </summary>
        /// <param name="pca">Public client app</param>
        /// <returns>Returns authentication result with silent token</returns>
        private async Task<AuthenticationResult> AcquireTokenSilentAsync(IPublicClientApplication pca)
        {
            _logger.LogInformation("Acquire token - Try acquire silent token");

            return await pca.AcquireTokenSilent(_scopes, (await pca.GetAccountsAsync()).FirstOrDefault()).ExecuteAsync();
        }

        /// <summary>
        /// A request for a device code token is generated.
        /// </summary>
        /// <param name="pca">Public client app</param>
        /// <returns>Returns authentication result with device token</returns>
        private async Task<AuthenticationResult> AcquireTokenWithDeviceCodeAsync(IPublicClientApplication pca)
        {
            _logger.LogInformation("Acquire token - Generate new token by device code.");

            return await pca.AcquireTokenWithDeviceCode(_scopes, deviceCodeCallback =>
            {
                _logger.LogInformation(deviceCodeCallback.Message);
                return Task.CompletedTask;
            }).ExecuteAsync();
        }
    }
}
