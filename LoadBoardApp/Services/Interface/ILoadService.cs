using LoadBoardApp.ViewModels.Common;

namespace LoadBoardApp.Services.Interface
{
    public interface ILoadService
    {
        IReadOnlyList<LoadViewModel> GetLoads(int curentPage, int itemsPerPage);
        int GetTotalLoadsCount();
        int ItemsPerPage();
        LoadViewModel GetPopUpItemById(int loadId);
    }
}
