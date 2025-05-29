namespace OneDriveFileBackuper.Options
{
    /// <summary>
    /// Option for backup storage
    /// </summary>
    public class LocalStorageOption
    {
        /// <summary>
        /// Name of section
        /// </summary>
        public const string ConfigurationSectionName = "LocalStorage";

        /// <summary>
        /// Path to save files
        /// </summary>
        public string? Path { get; set; }
    }
}
