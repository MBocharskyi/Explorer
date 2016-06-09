using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Explorer.ViewModels;

namespace Explorer.Services
{
    /// <summary>
    /// Defines an implementation of<see cref="IExplorerService"/>
    /// </summary>
    public class ExplorerService : IExplorerService
    {
        private const long ONE_MB = 1024 * 1024;

        private const int TEN = 10;

        private const int FIFTY = 50;

        private const int ONE_HUNDRED = 100;

        private DirectoryInfo _directoryInfo;

        /// <summary>
        /// Gets and sets count of files with size less than 10mb
        /// </summary>
        public int CountOfFilesLessThanTenMb { get; set; }

        /// <summary>
        /// Gets and sets count of files with size more than 10mb and less than 51mb
        /// </summary>
        public int CountOfFilesMoreThanTenAndLessThanFiftyOneMb { get; set; }

        /// <summary>
        /// Gets and sets count of files with size more than 100mb
        /// </summary>
        public int CountOfFilesMoreThanOneHundredMb { get; set; }

        /// <summary>
        /// Gets list of drives
        /// </summary>
        /// <returns>Returns list of drives</returns>
        public IEnumerable<string> GetDrives()
        {
            return DriveInfo.GetDrives()
                .Where(dr => dr.IsReady)
                .OrderBy(dr => dr.Name)
                .Select(dr => dr.Name)
                .ToList();
        }

        /// <summary>
        /// Gets list of directories by path
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <returns>Returns list of all directories in directory set by path</returns>
        public IEnumerable<DirectoryViewModel> GetDirectories(string path)
        {
            var directoryInfo = GetDirectoryInfo(path);


            return directoryInfo.EnumerateDirectories()
                .Where(dr => !dr.Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System))
                .Where(dr => !dr.Attributes.HasFlag(FileAttributes.Hidden))
                .OrderBy(dr => dr.Name)
                .Select(dr => new DirectoryViewModel { Name = dr.Name, FullPath = dr.FullName })
                .ToList();
        }

        /// <summary>
        /// Checks is directory exists
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <returns>True if directory exists, false if no</returns>
        public bool IsExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// Gets all files in directory
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <returns>Returns list of files in directory </returns>
        public IEnumerable<FileViewModel> GetFiles(string path)
        {
            var directoryInfo = GetDirectoryInfo(path);
            //DirectoryInfo directoryInfo = new DirectoryInfo(path);
            return directoryInfo.EnumerateFiles()
                .Where(dr => !dr.Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System))
                .Where(dr => !dr.Attributes.HasFlag(FileAttributes.Hidden))
                .OrderBy(dr => dr.Name)
                .Select(dr => new FileViewModel { Name = dr.Name, FullPath = dr.FullName })
                .ToList();
        }

        /// <summary>
        /// Gets path to parent folder
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <returns>Returns parent directory path for directory set by path
        /// If there is no parent folder returns empty string</returns>
        public string GetParentPath(string path)
        {
            var directoryInfo = GetDirectoryInfo(path);
            //DirectoryInfo directoryInfo = new DirectoryInfo(path);
            return directoryInfo.Parent != null ? directoryInfo.Parent.FullName : string.Empty;
        }

        /// <summary>
        /// Calculate files count in folder
        /// </summary>
        /// <param name="path">Directory path</param>
        public void CalculateFilesCount(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            OpenSubDirectories(directoryInfo);
            var files = GetFilesInfo(directoryInfo);
            CalculateFiles(files);
        }

        /// <summary>
        /// Calculate files in collection of files
        /// </summary>
        /// <param name="files">Collection of files</param>
        private void CalculateFiles(IEnumerable<FileInfo> files)
        {
            CountOfFilesLessThanTenMb += files.Count(fi => fi.Length / ONE_MB <= TEN);
            CountOfFilesMoreThanTenAndLessThanFiftyOneMb += files.Count(fi => fi.Length / ONE_MB > TEN && fi.Length / ONE_MB <= FIFTY);
            CountOfFilesMoreThanOneHundredMb += files.Count(fi => fi.Length / ONE_MB >= ONE_HUNDRED);
        }

        /// <summary>
        /// Get collection of files in directory
        /// </summary>
        /// <param name="directoryInfo">DirectoryInfo object with info about folder</param>
        /// <returns>List of files</returns>
        private IEnumerable<FileInfo> GetFilesInfo(DirectoryInfo directoryInfo)
        {
            return directoryInfo.EnumerateFiles()
                .Where(dr => !dr.Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System))
                .Where(dr => !dr.Attributes.HasFlag(FileAttributes.Hidden))
                .ToList();
        }

        /// <summary>
        /// Get all folders in folder and then open folder which accessible
        /// </summary>
        /// <param name="directoryInfo">DirectoryInfo object with info about folder</param>
        private void OpenSubDirectories(DirectoryInfo directoryInfo)
        {

            var directories = directoryInfo.EnumerateDirectories()
                .Where(dr => !dr.Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System))
                .Where(dr => !dr.Attributes.HasFlag(FileAttributes.Hidden))
                .ToList();

            foreach (var directory in directories)
            {
                try
                {
                    CalculateFilesCount(directory.FullName);
                }
                catch (UnauthorizedAccessException)
                {
                    // Not allowed to open directory.Skip and go to next one.
                }
            }

        }

        /// <summary>
        /// Checks if DirectoryInfo for folder exists returns it.
        /// If not gets DirectoryInfo and returns.
        /// </summary>
        /// <param name="path">Path to the directory</param>
        /// <returns>Returns DirectoryInfo for folder</returns>
        private DirectoryInfo GetDirectoryInfo(string path)
        {
            if (_directoryInfo == null)
            {
                _directoryInfo = new DirectoryInfo(path);
            }
            return _directoryInfo;
        }
    }
}