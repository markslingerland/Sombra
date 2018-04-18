using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL.Configurations
{
    public class CredentialTypeEntityTypeConfiguration : EntityTypeConfiguration<CredentialType> {
        public override void Configure(EntityTypeBuilder<CredentialType> entity)
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(64);
        }
    }
}