using System;

namespace Sombra.Messaging.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CachableAttribute : Attribute
    {
        public TimeSpan LifeTime = TimeSpan.FromDays(1);
    }
}