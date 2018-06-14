using System.Collections.Generic;

namespace Sombra.Web.ViewModels.Charity
{
    public class SearchResultsViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNumberOfResults { get; set; }
        public List<SearchResultViewModel> Results { get; set; }
    }
}