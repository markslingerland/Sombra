using System;

namespace Sombra.Messaging
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CachableAttribute : Attribute
    {
        public TimeSpan LifeTime { get; set; } = TimeSpan.FromDays(1);

        public int LifeTimeInDays
        {
            get => _lifeTimeInDays;
            set => LifeTime = TimeSpan.FromDays(_lifeTimeInDays = value);
        }

        public int LifeTimeInHours
{
            get => _lifeTimeInHours;
            set => LifeTime = TimeSpan.FromHours(_lifeTimeInHours = value);
        }

        public int LifeTimeInMinutes
        {
            get => _lifeTimeInMinutes;
            set => LifeTime = TimeSpan.FromMinutes(_lifeTimeInMinutes = value);
        }

        public int LifeTimeInSeconds
        {
            get => _lifeTimeInSeconds;
            set => LifeTime = TimeSpan.FromSeconds(_lifeTimeInSeconds = value);
        }

        private int _lifeTimeInDays;
        private int _lifeTimeInHours;
        private int _lifeTimeInMinutes;
        private int _lifeTimeInSeconds;
    }
}