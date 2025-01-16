using LoadBoardApp.PublishedContentModels.Models;
using LoadBoardApp.Services.Interface;
using LoadBoardApp.ViewModels.Common;
using LoadBoardApp.ViewModels.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

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
            var items = _home.Children.OfType<Load>().OrderByDescending(item => item.UpdateDate).Skip(ItemsPerPage() * (currentPage - 1)).Take(ItemsPerPage());
            var totalPages = (int)Math.Ceiling((double)GetTotalLoadsCount() / ItemsPerPage());

            return LoadsListing(items.ToViewModel(), currentPage, totalPages, ItemsPerPage());
        }
     
        public LoadsListingViewModel SearchLoadsByName(string search, int currentPage)
        {
            var query = _home.Children.OfType<Load>()
                .Where(load => string.IsNullOrEmpty(search) ||
                load.City.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                load.DeliveryCity.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                load.Broker.Contains(search, StringComparison.OrdinalIgnoreCase));

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / ItemsPerPage());

            var paginatedItems = query.OrderByDescending(item => item.UpdateDate).Skip(ItemsPerPage() * (currentPage - 1))
                                        .Take(ItemsPerPage())
                                        .ToViewModel();
    
            return LoadsListing(paginatedItems, currentPage, totalPages, ItemsPerPage());
        }

        public LoadsListingViewModel LoadsListing(IReadOnlyList<LoadViewModel> items, int currentPage, int totalPages, int itemsPerPage)
        {
            var model = new LoadsListingViewModel
            {
                Items = items,
                CurrentPage = currentPage,
                TotalPages = totalPages,
                ItemsPerPage = itemsPerPage,
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
