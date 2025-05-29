namespace OneDrivePhotoDownloader.Options
{
    /// <summary>
    /// Option for entra id
    /// </summary>
    public class EntraIdOption
    {
        /// <summary>
        /// Name of section
        /// </summary>
        public const string ConfigurationSectionName = "EntraId";

        /// <summary>
        /// Client id of application
        /// </summary>
        public string? ClientId { get; set; }
    }
}
