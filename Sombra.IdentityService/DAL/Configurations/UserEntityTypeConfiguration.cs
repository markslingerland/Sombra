using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.IdentityService.DAL;
using Sombra.Infrastructure.DAL;

namespace Sombra.IdentityService 
{
    class UserEntityTypeConfiguration : EntityTypeConfiguration<User> {
        public override void Configure(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(64);
        }
    }
}