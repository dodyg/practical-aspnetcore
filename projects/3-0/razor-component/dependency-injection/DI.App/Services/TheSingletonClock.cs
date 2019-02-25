using System;

namespace DI.App
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