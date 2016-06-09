using System.Collections.Generic;
using Explorer.ViewModels;

namespace Explorer.Services
{
    /// <summary>
    /// Interface for Files service
    /// </summary>
    public interface IFilesService
    {
        /// <summary>
        /// Gets and sets count of files with size less than 10mb
        /// </summary>
        int CountOfFilesLessThanTenMb { get; set; }

        /// <summary>
        /// Gets and sets count of files with size more than 10mb and less than 51mb
        /// </summary>
        int CountOfFilesMoreThanTenAndLessThanFiftyOneMb { get; set; }

        /// <summary>
        /// Gets and sets count of files with size more than 100mb
        /// </summary>
        int CountOfFilesMoreThanOneHundredMb { get; set; }

        /// <summary>
        /// Gets all files in directory
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <returns>Returns list of files in directory </returns>
        IEnumerable<FileViewModel> GetFiles(string path);

        /// <summary>
        /// Calculate files count in folder
        /// </summary>
        /// <param name="path">Directory path</param>
        void CalculateFilesCount(string path);
    }
}