using System.Collections.Generic;

namespace Explorer.Services
{
    /// <summary>
    /// Interface for Drive service
    /// </summary>
    public interface IDriveService
    {
        /// <summary>
        /// Gets list of drives
        /// </summary>
        /// <returns>Returns list of drives</returns>
        IEnumerable<string> GetDrives();
    }
}