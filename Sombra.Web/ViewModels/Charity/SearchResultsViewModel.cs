﻿using System.Collections.Generic;
using Sombra.Web.ViewModels.Shared;

namespace Sombra.Web.ViewModels.Charity
{
    public class SearchResultsViewModel : PagedViewModel
    {
        public List<SearchResultViewModel> Results { get; set; }
    }
}