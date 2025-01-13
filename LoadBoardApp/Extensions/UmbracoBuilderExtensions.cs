using LoadBoardApp.ContentFinders;
using Umbraco.Cms.Core.Routing;

namespace LoadBoardApp.Extensions
{
    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddCustomContentFinders(this IUmbracoBuilder builder)
        {
            builder.ContentFinders().Append<LoadDetailsContentFinder>();
           
            return builder;
        }
    }
}
