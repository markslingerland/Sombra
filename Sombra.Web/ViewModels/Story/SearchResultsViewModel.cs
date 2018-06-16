using System.Collections.Generic;
using Sombra.Web.ViewModels.Shared;

namespace Sombra.Web.ViewModels.Story
{
    public class SearchResultsViewModel : PagedViewModel
    {
        public List<SearchResultViewModel> Results { get; set; }
    }
}