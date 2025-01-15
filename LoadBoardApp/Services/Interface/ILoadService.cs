using LoadBoardApp.ViewModels.Common;

namespace LoadBoardApp.Services.Interface
{
    public interface ILoadService
    {
        LoadsListingViewModel GetLoads(int curentPage);
        LoadsListingViewModel SearchLoadsByName(string search, int currentPage);
        int GetTotalLoadsCount();
        int ItemsPerPage();
        LoadViewModel GetPopUpItemById(int loadId);
    }
}
