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
        public LoadDetailsContentFinder(IUmbracoContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        Task<bool> IContentFinder.TryFindContent(IPublishedRequestBuilder request)
        {
            if (!_contextAccessor.TryGetUmbracoContext(out var umbracoContext))
            {
                return Task.FromResult(false);
            }
            var content = umbracoContext.Content?.GetAtRoot().FirstOrDefault();

            if (content is null)
            {
                return Task.FromResult(false);
            }

            request.IgnorePublishedContentCollisions();
            request.SetPublishedContent(content);
            return Task.FromResult(true);
        }
    }
}
