using LoadBoardApp.Services.Interface;
using LoadBoardApp.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;

namespace LoadBoardApp.ViewComponents
{
    public class LoadViewComponent : ViewComponent
    {
        private readonly ILoadService _loadService;

        public LoadViewComponent(ILoadService loadService)
        {
            _loadService = loadService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int currentPage, int itemsPerPage)
        {
            var totalItems = _loadService.GetTotalLoadsCount();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            var model = new LoadsListingViewModel
            { 
                Items = _loadService.GetLoads(currentPage, totalPages, itemsPerPage),
                CurrentPage = currentPage,
                TotalPages = totalPages,
                ItemsPerPage = itemsPerPage
            };
            return View(model);        
        }
    }
}
