using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL.Configurations
{
    class PermissionEntityTypeConfiguration : EntityTypeConfiguration<Permission> {
        public override void Configure(EntityTypeBuilder<Permission> entity)
        {
            entity.Property(e => e.Code).IsRequired().HasMaxLength(32);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(64);
        }
    }
}