using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.Infrastructure.DAL;

namespace Sombra.StoryService.DAL.Configurations
{
    public class StoryEntityTypeConfiguration : EntityTypeConfiguration<Story>
    {
        public override void Configure(EntityTypeBuilder<Story> entity)
        {
            entity.HasIndex(e => e.StoryKey);
            entity.HasIndex(e => e.UrlComponent);
        }
    }
}