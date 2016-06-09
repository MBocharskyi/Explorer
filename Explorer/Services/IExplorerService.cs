namespace Explorer.Services
{
    /// <summary>
    /// Interface for explorer service
    /// </summary>
    public interface IExplorerService : IFilesService, IDriveService, IDirectoryService
    {
    }
}
