using LoadBoardApp.ViewModels.Common;

namespace LoadBoardApp.Services.Interface
{
    public interface ILoadService
    {
        IReadOnlyList<LoadViewModel> GetLoads(int curentPage, int totalPages, int itemsPerPage);
        int GetTotalLoadsCount();
    }
}
