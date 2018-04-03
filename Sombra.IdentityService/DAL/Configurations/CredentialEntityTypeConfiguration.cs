using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL.Configurations
{
    class CredentialEntityTypeConfiguration : EntityTypeConfiguration<Credential> {
        public override void Configure(EntityTypeBuilder<Credential> entity)
        {
            entity.Property(e => e.Identifier).IsRequired().HasMaxLength(64);
            entity.Property(e => e.Secret).HasMaxLength(1024);
        }
    }
}