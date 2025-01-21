namespace LoadBoardApp.ViewModels.Common
{
    public class LoadsListingViewModel
    {
        public LoadsListingViewModel(IReadOnlyList<LoadViewModel> items, int currentPage, int totalPages, int itemsPerPage)
        {
            Items = items;
            CurrentPage = currentPage;
            TotalPages = totalPages;
            ItemsPerPage = itemsPerPage;
        }
        public IReadOnlyList<LoadViewModel> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
