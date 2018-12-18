using System;
using System.Threading;
using DotNetify;

namespace Dotnetify_ReactJs
{
    public class DemoVM : BaseVM
    {
        private readonly Timer _timer;
        private int _counter;

        public DemoVM()
        {
            _timer = new Timer((_) =>
            {
                Changed(nameof(Now));
                Changed(nameof(Counter));
                
                PushUpdates();
            }, state: null, dueTime: 0, period: 1_000);
        }

        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        public int Counter
        {
            get
            {
                return _counter;
            }
        }

        public Action IncrementCounter => () => {
            _counter++;
        };
    }
}