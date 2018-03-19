using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.IdentityService.DAL;

namespace Sombra.IdentityService 
{
    class UserRolEntityTypeConfiguration : DbEntityConfiguration<UserRole> {
        public override void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });
            entity.ToTable("UserRoles");
        }
    }
}