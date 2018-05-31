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
        public Guid UserKey { get; set; }
        public Guid CharityId { get; set; }
        public virtual Charity Charity { get; set; }

    }
}