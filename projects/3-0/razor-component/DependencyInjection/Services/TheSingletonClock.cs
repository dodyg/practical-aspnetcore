using System;

namespace DependencyInjection.Services
{
    public class TheSingletonClock
    {
        DateTime _clock = DateTime.UtcNow;

        public string Get()
        {
            return _clock.ToString();
        }
    }
}