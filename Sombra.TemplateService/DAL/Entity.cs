using System;

namespace Sombra.TemplateService.Templates.DAL
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}