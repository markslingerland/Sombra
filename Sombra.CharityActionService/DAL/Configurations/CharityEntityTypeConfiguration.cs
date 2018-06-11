using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.Infrastructure.DAL;

namespace Sombra.CharityActionService.DAL.Configurations
{
    public class CharityEntityTypeConfiguration : EntityTypeConfiguration<Charity>
    {
        public override void Configure(EntityTypeBuilder<Charity> entity)
        {
            entity.HasIndex(e => e.CharityKey);
            entity.HasIndex(e => e.Url);
        }
    }
}