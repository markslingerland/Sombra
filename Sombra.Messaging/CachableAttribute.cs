using System;

namespace Sombra.Messaging
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CachableAttribute : Attribute
    {
        public TimeSpan LifeTime = TimeSpan.FromDays(1);
    }
}