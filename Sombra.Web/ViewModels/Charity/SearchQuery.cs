using System.Collections.Generic;
using Sombra.Core.Enums;

namespace Sombra.Web.ViewModels.Charity
{
    public class SearchQuery
    {
        public List<string> Keywords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 9;
        public Category Category { get; set; }
    }
}
