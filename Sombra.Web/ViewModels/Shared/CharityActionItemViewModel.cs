using System;

namespace Sombra.Web.ViewModels.Shared
{
    public class CharityActionItemViewModel
    {
        public Guid CharityActionKey { get; set; }
        public string Name { get; set; }
        public string CoverImage { get; set; }
        public string OrganiserImage { get; set; }
        public string OrganiserUserName { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CollectedAmount { get; set; }
        public DateTime ActionEndDateTime { get; set; }
    }
}