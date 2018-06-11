using System;
using AutoMapper;

namespace Sombra.Infrastructure.DAL
{
    public abstract class Entity : IEntity
    {
        [IgnoreMap]
        public Guid Id { get; set; }
    }
}