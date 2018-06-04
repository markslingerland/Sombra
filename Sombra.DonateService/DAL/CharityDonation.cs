using System;
using Sombra.Core.Enums;
using Sombra.Infrastructure.DAL;

namespace Sombra.DonateService.DAL
{
    public class CharityDonation : Entity
    {
        public DateTime DateTimeStamp { get; set; }
        public decimal Amount { get; set; }
        public DonationType DonationType { get; set; }
        public bool IsAnonymous { get; set; }
        public Guid CharityId { get; set; }
        public virtual Charity Charity { get; set; }
        public virtual User User { get; set; }
        public Guid? UserId { get; set; }
    }
}