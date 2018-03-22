using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.IdentityService.DAL;

namespace Sombra.IdentityService 
{
    class RolePermissionEntityTypeConfiguration : DbEntityConfiguration<RolePermission> {
        public override void Configure(EntityTypeBuilder<RolePermission> entity)
        {
            entity.HasKey(e => new { e.RoleId, e.PermissionId });

            entity.HasOne(e => e.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            entity.HasOne(e => e.Permission)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            entity.ToTable("RolePermissions");
        }
    }
}