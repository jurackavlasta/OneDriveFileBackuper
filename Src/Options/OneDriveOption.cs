namespace OneDrivePhotoDownloader.Options
{
    /// <summary>
    /// One drive options
    /// </summary>
    public class OneDriveOption
    {
        public const string ConfigurationSectionName = "OneDrive";

        /// <summary>
        /// Drive of one drive
        /// </summary>
        public string? Drive { get; set; }

        /// <summary>
        /// ID of folder with files
        /// </summary>
        public string? FilesFolder { get; set; }
    }
}
