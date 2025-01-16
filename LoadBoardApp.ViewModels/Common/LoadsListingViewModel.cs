namespace LoadBoardApp.ViewModels.Common
{
    public class LoadsListingViewModel
    {
        public IReadOnlyList<LoadViewModel> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
        public string SearchTerm { get; set; }
    }
}
