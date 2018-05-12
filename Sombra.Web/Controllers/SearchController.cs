using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sombra.Web.ViewModels.Search;

namespace Sombra.Web.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetTopCharities(TopCharitiesQuery query)
        {
            var result = new TopCharitiesViewModel
            {
                Charities = new List<CharityItemViewModel>
                {
                    new CharityItemViewModel(),
                    new CharityItemViewModel(),
                    new CharityItemViewModel()
                }
            };

            return PartialView("_ActionItemsWrapper", result);
        }
    }
}