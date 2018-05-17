using System;
using Sombra.Core.Enums;

namespace Sombra.Web.ViewModels.Search
{
    public class CharityItemViewModel
    {
        public Guid Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Category Category { get; set; }
    }
}