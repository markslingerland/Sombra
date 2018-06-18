using System;
using Sombra.Core.Enums;

namespace Sombra.Web.ViewModels.Charity
{
    public class CharityViewModel
    {
        public Guid CharityKey { get; set; }
        public string CoverImage { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string KVKNumber { get; set; }
        public string Anbi { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
    }
}