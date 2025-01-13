using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBoardApp.ViewModels.Common
{
    public class LoadsListingViewModel
    {
        public LoadsListingViewModel() { }
        public IReadOnlyList<LoadViewModel> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
