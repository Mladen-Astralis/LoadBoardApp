using Examine.Search;
using LoadBoardApp.ViewModels.Common;
using Umbraco.Cms.Infrastructure.Examine;

namespace LoadBoardApp.Services.Interface
{
    public interface ISearchService
    {
        LoadsListingViewModel Search(string query, int page, string searchType = IndexTypes.Content, BooleanOperation searchOperation = BooleanOperation.And);
    }
}
