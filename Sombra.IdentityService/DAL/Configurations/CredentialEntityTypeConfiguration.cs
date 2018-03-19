using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.IdentityService.DAL;

namespace Sombra.IdentityService 
{
    class CredentialEntityTypeConfiguration : DbEntityConfiguration<Credential> {
        public override void Configure(EntityTypeBuilder<Credential> entity)
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Identifier).IsRequired().HasMaxLength(64);
            entity.Property(e => e.Secret).HasMaxLength(1024);
            entity.ToTable("Credentials");
        }
    }
}