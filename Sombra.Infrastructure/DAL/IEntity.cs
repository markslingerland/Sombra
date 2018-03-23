using System;

namespace Sombra.Infrastructure.DAL
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}