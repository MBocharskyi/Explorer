using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using Explorer.Services;
using Explorer.ViewModels;

namespace Explorer.Controllers
{
    /// <summary>
    /// Explorer controller
    /// </summary>
    public class FileExplorerController : ApiController
    {
        private readonly IExplorerService _explorerService;

        /// <summary>
        /// Initialized a new instance of <see cref="FileExplorerController"/> class
        /// </summary>
        /// <param name="explorerService">Explorer service</param>
        public FileExplorerController(IExplorerService explorerService)
        {
            _explorerService = explorerService;
        }

        /// <summary>
        /// Gets folder content by path
        /// </summary>
        /// <param name="path">Path to the folder</param>
        /// <returns>ViewModel with info about folder content</returns>
        public DirectoriesFilesInfoViewModel Get(string path)
        {
            path = new JavaScriptSerializer().Deserialize<string>(path);
            var viewModel = new DirectoriesFilesInfoViewModel();

            if (string.IsNullOrEmpty(path))
            {
                viewModel.Drives = _explorerService.GetDrives();
            }
            else
            {
                if (_explorerService.IsExists(path))
                {
                    viewModel.Directories = _explorerService.GetDirectories(path);
                    viewModel.Files = _explorerService.GetFiles(path);
                    _explorerService.CalculateFilesCount(path);
                    viewModel.CountOfFilesLessThanTenMb = _explorerService.CountOfFilesLessThanTenMb;
                    viewModel.CountOfFilesMoreThanTenAndLessThanFiftyOneMb =
                        _explorerService.CountOfFilesMoreThanTenAndLessThanFiftyOneMb;
                    viewModel.CountOfFilesMoreThanOneHundredMb = _explorerService.CountOfFilesMoreThanOneHundredMb;
                    viewModel.CurrentPath = path;
                    viewModel.ParentPath = _explorerService.GetParentPath(path);
                }
                else
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No Such Directory"));
                }
            }

            return viewModel;
        }
    }
}