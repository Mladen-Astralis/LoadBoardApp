using LoadBoardApp.Services.Interface;
using LoadBoardApp.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace LoadBoardApp.Controllers
{
    public class HomeController : SurfaceController
    {
        private readonly ILoadService _loadService;

        public HomeController(IUmbracoContextAccessor umbracoContextAccessor, 
            IUmbracoDatabaseFactory databaseFactory, 
            ServiceContext services, AppCaches appCaches, 
            IProfilingLogger profilingLogger, 
            IPublishedUrlProvider publishedUrlProvider,
            ILoadService loadService) 
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _loadService = loadService;
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult GetPaginatedLoads(int currentPage, int itemsPerPage)
        {
            var totalItems = _loadService.GetTotalLoadsCount();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            var model = new LoadsListingViewModel
            {
                Items = _loadService.GetLoads(currentPage, itemsPerPage),
                CurrentPage = currentPage,
                TotalPages = totalPages,
                ItemsPerPage = itemsPerPage
            };
            return PartialView("Items/_LoadsPagination", model);
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult GetPopUpItem(int loadId)
        {
            var item = _loadService.GetPopUpItemById(loadId);
            return PartialView("PopUp/_PopUpView", item);
        }

    }
}
