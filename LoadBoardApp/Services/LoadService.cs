using LoadBoardApp.PublishedContentModels.Models;
using LoadBoardApp.Services.Interface;
using LoadBoardApp.ViewModels.Common;
using LoadBoardApp.ViewModels.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.UmbracoContext;

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

      public IReadOnlyList<LoadViewModel> GetLoads(int currentPage, int itemsPerPage)
      {
        var loads = _home.Children.OfType<Load>().Skip(itemsPerPage * (currentPage - 1)).Take(itemsPerPage);
        return loads.ToViewModel();
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
