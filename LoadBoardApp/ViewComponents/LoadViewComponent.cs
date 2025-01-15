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
        public async Task<IViewComponentResult> InvokeAsync(int page = 1)
        {
            var itemsPerPage = _loadService.ItemsPerPage();
            var totalItems = _loadService.GetTotalLoadsCount();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            var model = new LoadsListingViewModel
            { 
                Items = _loadService.GetLoads(page, itemsPerPage),
                CurrentPage = page,
                TotalPages = totalPages,
                ItemsPerPage = itemsPerPage
            };
            return View(model);        
        }
    }
}
