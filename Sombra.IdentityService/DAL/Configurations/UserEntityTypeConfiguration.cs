using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService.DAL.Configurations
{
    public class UserEntityTypeConfiguration : EntityTypeConfiguration<User> {
        public override void Configure(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(64);
            entity.HasIndex(e => e.UserKey);
        }
    }
}