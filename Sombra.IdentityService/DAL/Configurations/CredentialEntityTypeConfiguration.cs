using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.IdentityService.DAL;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService 
{
    class CredentialEntityTypeConfiguration : EntityTypeConfiguration<Credential> {
        public override void Configure(EntityTypeBuilder<Credential> entity)
        {
            entity.Property(e => e.Identifier).IsRequired().HasMaxLength(64);
            entity.Property(e => e.Secret).HasMaxLength(1024);
            entity.ToTable("Credentials");
        }
    }
}