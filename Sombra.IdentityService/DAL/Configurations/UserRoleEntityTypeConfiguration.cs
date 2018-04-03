using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL.Configurations
{
    class UserRoleEntityTypeConfiguration : EntityTypeConfiguration<UserRole> {
        public override void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.HasOne(e => e.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            entity.HasOne(e => e.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId);
        }
    }
}