using System;
using System.ComponentModel.DataAnnotations;

namespace Sombra.Core
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}