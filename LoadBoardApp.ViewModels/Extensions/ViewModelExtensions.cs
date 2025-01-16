using LoadBoardApp.Common.Extensions;
using LoadBoardApp.PublishedContentModels.Models;
using LoadBoardApp.ViewModels.Common;

namespace LoadBoardApp.ViewModels.Extensions
{
    public static class ViewModelExtensions
    {
        public static IReadOnlyList<LoadViewModel> ToViewModel(this IEnumerable<Load> items)
          => items.HasValue() ? items?.Select(item => new LoadViewModel(item)).OrderByDescending(item => item.CreateDate).ToList() : new List<LoadViewModel>();
    }
}
