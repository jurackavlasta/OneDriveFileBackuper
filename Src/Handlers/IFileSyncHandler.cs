using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
