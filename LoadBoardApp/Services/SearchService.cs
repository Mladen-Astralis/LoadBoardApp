using System.ComponentModel;
using Examine;
using Examine.Lucene.Search;
using Examine.Search;
using LoadBoardApp.Common;
using LoadBoardApp.PublishedContentModels.Models;
using LoadBoardApp.Services.Interface;
using LoadBoardApp.ViewModels.Common;
using LoadBoardApp.ViewModels.Extensions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Examine;

namespace LoadBoardApp.Services
{
    public class SearchService : ISearchService
    {
        private readonly IExamineManager _examineManager;
        private readonly string[] _searchFields;
        private readonly ISearcher _searcher;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IUmbracoContext _umbracoContext;
        private readonly IPublishedContent _home;

        public SearchService(IExamineManager examineManager, IUmbracoContextAccessor umbracoContextAccessor)
        {
            _examineManager = examineManager ?? throw new ArgumentNullException(nameof(examineManager));
            _searcher = GetSearcher(Constants.ExamineIndexes.ExternalIndex);
            _searchFields = new[] { Constants.Fields.NodeName, Constants.Fields.City, Constants.Fields.DeliveryCity, Constants.Fields.Broker };
            _umbracoContextAccessor = umbracoContextAccessor;
            _umbracoContext = _umbracoContextAccessor.GetRequiredUmbracoContext();
            _home = _umbracoContext.Content?.GetAtRoot()?.OfType<Home>().FirstOrDefault();
        }

        public LoadsListingViewModel Search(string query, int page, 
            string searchType = IndexTypes.Content,
            BooleanOperation searchOperation = BooleanOperation.And)
        {
            var home = _home as Home;
            var itemsPerPage = home?.LoadsNumber ?? Constants.ItemsPerPage.ItemsNumber;

            if (page < 0) throw new ArgumentOutOfRangeException(nameof(page));
            if (!Enum.IsDefined(typeof(BooleanOperation), searchOperation))
            {
                throw new InvalidEnumArgumentException(nameof(searchOperation), (int)searchOperation, typeof(BooleanOperation));
            }
            ValidateSearchType(searchType);

            var results = CreateLuceneSearchQuery(searchType, searchOperation)
                .NativeQuery(BuildQuery(query, searchType, searchOperation))
                .Execute();

            var totalPages = (int)Math.Ceiling((double)results?.TotalItemCount / itemsPerPage);

            _umbracoContextAccessor.TryGetUmbracoContext(out var context);

            var items = results.Skip(itemsPerPage * (page - 1))
               .Take(itemsPerPage)
               .ToPublishedSearchResults(context.Content)
               .Select(psr => (Load)psr.Content).ToViewModel();

            return new LoadsListingViewModel(items, page, totalPages, itemsPerPage);
        }

        private static void ValidateSearchType(string searchType)
        {
            if (string.IsNullOrWhiteSpace(searchType)) throw new ArgumentException(nameof(searchType));

            switch (searchType)
            {
                case IndexTypes.Content: return;
                case IndexTypes.Media: return;
                case IndexTypes.Member: return;
                default: throw new ArgumentException($"Not valid search type '{searchType}'.", nameof(searchType));
            }
        }

        private string BuildQuery(string query, string searchType, BooleanOperation searchOperation)
        {
            const int highBoostValue = 4;

            query = query.Trim('\"', '\'');
            IExamineValue wholeExamineValue = query.Boost(highBoostValue);
            var words = query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            IExamineValue[] wordsExamineValues = words.Select(w => w.Escape()).ToArray();

            LuceneSearchQuery luceneQuery = CreateLuceneSearchQuery(searchType, searchOperation);
            luceneQuery.Field("__NodeTypeAlias", nameof(Load)).And().Group(nestedQuery => nestedQuery.GroupedOr(_searchFields, wholeExamineValue).Or().GroupedOr(_searchFields, wordsExamineValues));

                     
            return luceneQuery.Query.ToString();
        }

        private LuceneSearchQuery CreateLuceneSearchQuery(string searchType, BooleanOperation searchOperation)
        {
            return (LuceneSearchQuery)_searcher.CreateQuery(searchType, searchOperation);
        }

        private ISearcher GetSearcher(string indexName)
        {
            if (!_examineManager.TryGetIndex(indexName, out var index))
            {
                throw new InvalidOperationException($"No index found by name {indexName}");
            }

            return index.Searcher;
        }

    }
}
