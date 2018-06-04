using System;
using Sombra.Core.Enums;
using Sombra.Infrastructure.DAL;

namespace Sombra.DonateService.DAL
{
    public class CharityActionDonation : Entity
    {
        public DateTime DateTimeStamp { get; set; }
        public decimal Amount { get; set; }
        public Guid UserKey { get; set; }
        public DonationType DonationType { get; set; }
        public bool IsAnonymous { get; set; }
        public Guid CharityActionId { get; set; }
        public virtual CharityAction CharityAction { get; set; }
    }
}