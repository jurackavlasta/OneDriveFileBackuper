namespace OneDriveFileBackuper.Auth
{
    /// <summary>
    /// Token provider for JWT token
    /// </summary>
    public interface IAuthJwtTokenProvider
    {
        /// <summary>
        /// Generate JWT token 
        /// </summary>
        /// <returns>Returns generated JWT token</returns>
        public Task<string> GenerateJwtTokenAsync();
    }
}
