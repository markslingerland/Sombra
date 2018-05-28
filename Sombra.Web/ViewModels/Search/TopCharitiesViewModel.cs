using System.Collections.Generic;

namespace Sombra.Web.ViewModels.Search
{
    public class TopCharitiesViewModel
    {
        public List<CharityItemViewModel> Charities { get; set; }
        public int RequestedAmount { get; set; }
    }
}