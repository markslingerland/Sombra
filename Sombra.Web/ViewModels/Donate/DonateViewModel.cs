using System;
using Sombra.Core.Enums;

namespace Sombra.Web.ViewModels.Donate
{
    public class DonateViewModel
    {
        public bool IsAnonymous { get; set; }
        public DonationType DonationType { get; set; }
        public decimal Amount { get; set; }
        public string CharityKey { get; set; }
        public string CharityActionKey { get; set; }
    }
}