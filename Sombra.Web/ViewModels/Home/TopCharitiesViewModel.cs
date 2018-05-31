using System.Collections.Generic;

namespace Sombra.Web.ViewModels.Home
{
    public class TopCharitiesViewModel
    {
        public List<CharityItemViewModel> Charities { get; set; }
        public int RequestedAmount { get; set; }
    }
}