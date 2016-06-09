using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Explorer.ViewModels
{
    /// <summary>
    /// Represents an abstract class with info about files and folders
    /// </summary>
    public abstract class DirectoryFileBaseViewModel
    {
        /// <summary>
        /// Gets or sets file name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets file full path
        /// </summary>
        public string FullPath { get; set; }
    }
}