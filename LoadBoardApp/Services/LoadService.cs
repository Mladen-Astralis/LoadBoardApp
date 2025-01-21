using LoadBoardApp.Common;
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
            _home = _umbracoContext.Content?.GetAtRoot()?.OfType<Home>().FirstOrDefault();
        }

        public LoadsListingViewModel GetLoads(int currentPage)
        {
            var home = _home as Home;
            var itemsPerPage = home?.LoadsNumber ?? Constants.ItemsPerPage.ItemsNumber;

            var getTotalLoadsCount = _home.Children?.OfType<Load>()?.Count() ?? 0;
            var items = _home.Children?.OfType<Load>().OrderByDescending(item => item.UpdateDate).Skip(itemsPerPage * (currentPage - 1)).Take(itemsPerPage);
            var totalPages = (int)Math.Ceiling((double)getTotalLoadsCount / itemsPerPage);

            return new LoadsListingViewModel(items.ToViewModel(), currentPage, totalPages, itemsPerPage);
        }
     
        public LoadsListingViewModel SearchLoadsByName(string search, int currentPage)
        {
            var home = _home as Home;
            var itemsPerPage = home?.LoadsNumber ?? Constants.ItemsPerPage.ItemsNumber;

            var query = _home.Children?.OfType<Load>()?
                .Where(load => load.City.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                load.DeliveryCity.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                load.Broker.Contains(search, StringComparison.OrdinalIgnoreCase));

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            var paginatedItems = query.OrderByDescending(item => item.UpdateDate).Skip(itemsPerPage * (currentPage - 1))
                                        .Take(itemsPerPage)
                                        .ToViewModel();
    
            return new LoadsListingViewModel(paginatedItems, currentPage, totalPages, itemsPerPage);
        }

        public LoadViewModel GetPopUpItemById(int loadId)
        {
            var content = _umbracoContext.Content?.GetById(loadId);
            var item = content as Load;
            if (item == null)
            {
                throw new ArgumentException($"Content with ID {loadId} is not of type Load or does not exist.");
            }
            return new LoadViewModel(item);
        }

    }
}
