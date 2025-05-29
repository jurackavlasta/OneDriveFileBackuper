namespace OneDriveFileBackuper.Options
{
    /// <summary>
    /// Option for backup storage
    /// </summary>
    public class BackupSettingsOption
    {
        /// <summary>
        /// Name of section
        /// </summary>
        public const string ConfigurationSectionName = "BackupSettings";

        /// <summary>
        /// Whether to delete files after backup
        /// </summary>
        public bool DeleteAfterBackup { get; set; } = false;
    }
}
