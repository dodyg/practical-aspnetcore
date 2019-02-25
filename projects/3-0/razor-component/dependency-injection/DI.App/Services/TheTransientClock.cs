using System;

namespace DI.App
{
    public class TheTransientClock
    {
        DateTime _clock = DateTime.UtcNow;

        public string Get()
        {
            return _clock.ToString();
        }
    }
}