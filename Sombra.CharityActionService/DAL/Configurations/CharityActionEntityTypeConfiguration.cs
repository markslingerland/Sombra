using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.Infrastructure.DAL;

namespace Sombra.CharityActionService.DAL.Configurations
{
    public class CharityActionEntityTypeConfiguration : EntityTypeConfiguration<CharityAction>
    {
        public override void Configure(EntityTypeBuilder<CharityAction> entity)
        {
            entity.HasIndex(c => c.CharityActionKey);
        }
    }
}