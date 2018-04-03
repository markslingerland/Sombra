using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.IdentityService.DAL;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService 
{
    class PermissionEntityTypeConfiguration : EntityTypeConfiguration<Permission> {
        public override void Configure(EntityTypeBuilder<Permission> entity)
        {
            entity.Property(e => e.Code).IsRequired().HasMaxLength(32);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(64);
        }
    }
}