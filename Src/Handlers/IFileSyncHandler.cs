namespace OneDriveFileBackuper.Handlers
{
    /// <summary>
    /// Handler for file sync from onedrive and save
    /// </summary>
    public interface IFileSyncHandler
    {
        /// <summary>
        /// Sync file from onedrive and backup
        /// </summary>
        /// <returns>Returns task</returns>
        public Task SyncAsync();
    }
}
