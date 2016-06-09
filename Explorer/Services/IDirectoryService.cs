using System.Collections.Generic;
using Explorer.ViewModels;

namespace Explorer.Services
{
    /// <summary>
    /// Interface for Directory service
    /// </summary>
    public interface IDirectoryService
    {
        /// <summary>
        /// Gets list of directories by path
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <returns>Returns list of all directories in directory set by path</returns>
        IEnumerable<DirectoryViewModel> GetDirectories(string path);

        /// <summary>
        /// Gets path to parent folder
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <returns>Returns parent directory path for directory set by path</returns>
        string GetParentPath(string path);

        /// <summary>
        /// Checks is directory exists
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <returns>True if directory exists, false if no</returns>
        bool IsExists(string path);
    }
}