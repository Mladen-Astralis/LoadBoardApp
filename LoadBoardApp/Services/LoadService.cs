using LoadBoardApp.PublishedContentModels.Models;
using LoadBoardApp.Services.Interface;
using LoadBoardApp.ViewModels.Common;
using LoadBoardApp.ViewModels.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.UmbracoContext;
using static Umbraco.Cms.Core.Constants.HttpContext;

namespace LoadBoardApp.Services
{
    public class LoadService : ILoadService
    {
        private readonly IUmbracoContextAccessor _contextAccessor;
        private readonly IUmbracoContext _umbracoContext;
        private readonly IPublishedContent _home;

        public LoadService(IUmbracoContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _umbracoContext = _contextAccessor.GetRequiredUmbracoContext();
            _home = _umbracoContext.Content?.GetAtRoot().FirstOrDefault();
        }

        public LoadsListingViewModel GetLoads(int currentPage)
        {
            var items = _home.Children.OfType<Load>().Skip(ItemsPerPage() * (currentPage - 1)).Take(ItemsPerPage());
            var totalPages = (int)Math.Ceiling((double)GetTotalLoadsCount() / ItemsPerPage());

            var model = new LoadsListingViewModel
            {
                Items = items.ToViewModel(),
                CurrentPage = currentPage,
                TotalPages = totalPages,
                ItemsPerPage = ItemsPerPage(),
            };
            return model;
        }

        public LoadsListingViewModel SearchLoadsByName(string search, int currentPage)
        {
            var query = _home.Children.OfType<Load>()
                .Where(load => string.IsNullOrEmpty(search) ||
                load.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                load.City.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                load.Broker.Contains(search, StringComparison.OrdinalIgnoreCase));

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / ItemsPerPage());

            var paginatedItems = query.Skip(ItemsPerPage() * (currentPage - 1))
                                        .Take(ItemsPerPage())
                                        .ToViewModel();

            var model = new LoadsListingViewModel
            {
                Items = paginatedItems,
                CurrentPage = currentPage,
                TotalPages = totalPages,
                ItemsPerPage = ItemsPerPage(),
                SearchTerm = search
            };

            return model;
        }

        public int GetTotalLoadsCount()
        {
            if (_home == null) return 0;
            return _home.Children.OfType<Load>().Count();
        }

        public LoadViewModel GetPopUpItemById(int loadId)
        {
            var content = _umbracoContext.Content?.GetById(loadId);
            var item = (Load)content;
            return new LoadViewModel(item);
        }

        public int ItemsPerPage()
        {
            var itemsPerPage = _home?.Value<int>("loadsNumber");
            return (int)itemsPerPage;
        }

    }
}
