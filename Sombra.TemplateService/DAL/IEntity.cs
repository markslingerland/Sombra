using System;
using System.ComponentModel.DataAnnotations;

namespace Sombra.TemplateService.Templates.DAL
{
    public interface IEntity
    {
        [Key]
        Guid Id { get; set; }
    }
}