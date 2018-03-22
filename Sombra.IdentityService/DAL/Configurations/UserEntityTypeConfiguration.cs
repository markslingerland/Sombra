using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.IdentityService.DAL;

namespace Sombra.IdentityService 
{
    class UserEntityTypeConfiguration : DbEntityConfiguration<User> {
        public override void Configure(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.Name).IsRequired().HasMaxLength(64);
            entity.ToTable("Users");
        }
    }
}