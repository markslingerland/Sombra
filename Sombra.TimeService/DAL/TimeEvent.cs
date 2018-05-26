using System;
using Sombra.Infrastructure.DAL;

namespace Sombra.TimeService.DAL
{
    public class TimeEvent : Entity
    {
        public DateTime Created { get; set; }
        public TimeInterval Type { get; set; }
    }
}