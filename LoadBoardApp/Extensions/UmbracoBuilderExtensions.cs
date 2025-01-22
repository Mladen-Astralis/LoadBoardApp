using LoadBoardApp.ContentFinders;
using LoadBoardApp.Services;
using LoadBoardApp.Services.Interface;

namespace LoadBoardApp.Extensions
{
    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddCustomContentFinders(this IUmbracoBuilder builder)
        {
            builder.ContentFinders().Append<LoadDetailsContentFinder>();
           
            return builder;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ILoadService, LoadService>();
            services.AddTransient<ISearchService, SearchService>();

            return services;
        }
    }
}
