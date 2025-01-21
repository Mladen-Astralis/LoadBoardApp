using LoadBoardApp.Services.Interface;
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
        private readonly ISearchService _searchService;

        public HomeController(IUmbracoContextAccessor umbracoContextAccessor, 
            IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider,
            ILoadService loadService,
            ISearchService searchService) 
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _loadService = loadService;
            _searchService = searchService;
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult GetPaginatedLoads(string search, int page = 1)
        {
            var model = _loadService.GetLoads(page);

            if (!string.IsNullOrEmpty(search))
            {
                model = _searchService.Search(search, page);
            }

            //if (!string.IsNullOrEmpty(search))
            //{
            //    model = _loadService.SearchLoadsByName(search, page);
            //}

            return PartialView("Items/_Loads", model);
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public IActionResult GetPopUpItem(int loadId)
        {
            if (loadId <= 0) throw new ArgumentOutOfRangeException(nameof(loadId));

            var item = _loadService.GetPopUpItemById(loadId);
            return PartialView("PopUp/_PopUpView", item);
        }
      
    }
}
