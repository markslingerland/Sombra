using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL.Configurations
{
    public class RoleEntityTypeConfiguration : EntityTypeConfiguration<Role> {
        public override void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(64);
        }
    }
}