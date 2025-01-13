using LoadBoardApp.PublishedContentModels.Models;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Persistence.EFCore;
using Umbraco.Cms.Web.Common.UmbracoContext;

namespace LoadBoardApp.ContentFinders
{
    public class LoadDetailsContentFinder : IContentFinder
    {
        private readonly IUmbracoContextAccessor _contextAccessor;
        private readonly IUmbracoContext _umbracoContext;
        public LoadDetailsContentFinder(IUmbracoContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _umbracoContext = _contextAccessor.GetRequiredUmbracoContext();
        }
        Task<bool> IContentFinder.TryFindContent(IPublishedRequestBuilder request)
        {
            var content = _umbracoContext.Content.GetAtRoot();

            //var home = _umbracoContext.Content?.GetAtRoot()?.FirstOrDefault();
            //var loads = home.Children.OfType<Load>();


            //if (content is null)
            //{
            //    return Task.FromResult(false);
            //}

            request.IgnorePublishedContentCollisions();
            //request.SetPublishedContent(content);
            return Task.FromResult(true);
        }
    }
}
