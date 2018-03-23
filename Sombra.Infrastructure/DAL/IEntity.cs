using System;
using System.ComponentModel.DataAnnotations;

namespace Sombra.Infrastructure.DAL
{
    public interface IEntity
    {
        [Key]
        Guid Id { get; set; }
    }
}