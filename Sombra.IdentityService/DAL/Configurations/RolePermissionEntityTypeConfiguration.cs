using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.IdentityService.DAL;

namespace Sombra.IdentityService 
{
    class RolePermissionEntityTypeConfiguration : DbEntityConfiguration<RolePermission> {
        public override void Configure(EntityTypeBuilder<RolePermission> entity)
        {
            entity.HasKey(e => new { e.RoleId, e.PermissionId });
            entity.ToTable("RolePermissions");
        }
    }
}