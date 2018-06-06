using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sombra.Infrastructure.DAL;

namespace Sombra.TemplateService.DAL.Configurations
{
    public class TemplateEntityTypeConfiguration : EntityTypeConfiguration<Template>
    {
        public override void Configure(EntityTypeBuilder<Template> entity)
        {
            entity.HasIndex(e => e.TemplateKey);
        }
    }
}