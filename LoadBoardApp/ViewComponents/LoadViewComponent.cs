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
            var model = _loadService.GetLoads(page);

            return View(model);        
        }
    }
}
