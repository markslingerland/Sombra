using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.IdentityService.DAL;

namespace Sombra.IdentityService 
{
    class CredentialTypeEntityTypeConfiguration : DbEntityConfiguration<CredentialType> {
        public override void Configure(EntityTypeBuilder<CredentialType> entity)
        {
            entity.Property(e => e.Code).IsRequired().HasMaxLength(32);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(64);
            entity.ToTable("CredentialTypes");
        }
    }
}