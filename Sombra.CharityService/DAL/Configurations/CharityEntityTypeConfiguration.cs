using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.Infrastructure.DAL;

namespace Sombra.CharityService.DAL.Configurations
{
    public class CharityEntityTypeConfiguration : EntityTypeConfiguration<Charity>
    {
        public override void Configure(EntityTypeBuilder<Charity> entity)
        {
            entity.HasIndex(c => c.CharityKey);
            entity.HasIndex(c => c.Url);
        }
    }
}