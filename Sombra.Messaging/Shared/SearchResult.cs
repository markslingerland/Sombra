using System;
using Sombra.Core.Enums;

namespace Sombra.Messaging.Shared
{
    public class SearchResult
    {
        public Guid CharityKey { get; set; }
        public Guid CharityActionKey { get; set; }
        public string CharityName { get; set; }
        public string CharityActionName { get; set; }
        public SearchContentType Type { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Category Category { get; set; }
    }
}