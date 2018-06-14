using System.Collections.Generic;
using Sombra.Infrastructure.DAL;

namespace Sombra.SearchService.DAL
{
    public class Content : Entity
    {
        public System.Guid CharityKey { get; set; }
        public System.Guid CharityActionKey { get; set; }
        public string CharityName { get; set; }
        public string CharityActionName { get; set; }
        public Core.Enums.SearchContentType Type { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Core.Enums.Category Category { get; set; }
        public string Url { get; set; }
        public string Logo { get; set; }
    }
}