using System;

namespace Sombra.Infrastructure.DAL
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}