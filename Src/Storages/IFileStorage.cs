namespace OneDriveFileBackuper.Storages
{
    /// <summary>
    /// Work with storage
    /// </summary>
    public interface IFileStorage
    {
        /// <summary>
        /// Save file by name and path in configuration
        /// </summary>
        /// <param name="name">Name of file</param>
        /// <param name="file">Content of file</param>
        /// <returns>Returns task</returns>
        public Task SaveFileAsync(string name, byte[] file);
    }
}
