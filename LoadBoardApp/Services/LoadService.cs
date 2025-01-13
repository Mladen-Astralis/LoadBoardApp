﻿using LoadBoardApp.PublishedContentModels.Models;
using LoadBoardApp.Services.Interface;
using LoadBoardApp.ViewModels.Common;
using LoadBoardApp.ViewModels.Extensions;
using Umbraco.Cms.Core.Web;

namespace LoadBoardApp.Services
{
    public class LoadService : ILoadService
    {
        private readonly IUmbracoContextAccessor _contextAccessor;
        private readonly IUmbracoContext _umbracoContext;

        public LoadService(IUmbracoContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _umbracoContext = _contextAccessor.GetRequiredUmbracoContext();
        }

      public IReadOnlyList<LoadViewModel> GetLoads(int currentPage, int totalPages, int itemsPerPage)
      {
        var home = _umbracoContext.Content?.GetAtRoot().FirstOrDefault();
        var loads = home.Children.OfType<Load>().Skip(itemsPerPage * (currentPage - 1)).Take(itemsPerPage);
        return loads.ToViewModel();
      }

      public int GetTotalLoadsCount()
      {
        var home = _umbracoContext.Content?.GetAtRoot().FirstOrDefault();

        if (home == null) return 0;

        return home.Children.OfType<Load>().Count();
      }


    }
}