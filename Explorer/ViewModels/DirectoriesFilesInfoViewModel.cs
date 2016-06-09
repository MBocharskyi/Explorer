using System.Collections.Generic;

namespace Explorer.ViewModels
{
    /// <summary>
    /// Represents a class with all needed info about files and folders
    /// </summary>
    public class DirectoriesFilesInfoViewModel
    {
        /// <summary>
        /// Gets or sets parent path
        /// </summary>
        public string ParentPath { get; set; }

        /// <summary>
        /// Gets or sets current path
        /// </summary>
        public string CurrentPath { get; set; }

        /// <summary>
        /// Gets or sets count of files less than 10mb
        /// </summary>
        public int CountOfFilesLessThanTenMb { get; set; }

        /// <summary>
        /// Gets or sets count of files more than 10mb and less than 51mb
        /// </summary>
        public int CountOfFilesMoreThanTenAndLessThanFiftyOneMb { get; set; }

        /// <summary>
        /// Gets or sets count of files more than 100mb
        /// </summary>
        public int CountOfFilesMoreThanOneHundredMb { get; set; }

        /// <summary>
        /// Gets or sets collection o drives
        /// </summary>
        public IEnumerable<string> Drives { get; set; }

        /// <summary>
        /// Gets or sets collection of directories
        /// </summary>
        public IEnumerable<DirectoryViewModel> Directories { get; set; }

        /// <summary>
        /// Gets or sets collection of files
        /// </summary>
        public IEnumerable<FileViewModel> Files { get; set; }
    }
}