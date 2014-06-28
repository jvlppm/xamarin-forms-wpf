using System;
using System.Threading;

namespace Xamarin.Forms.Platform.WPF
{
    class ThreadTimer : ITimer, IDisposable
    {
        readonly Timer _timer;

        public ThreadTimer(Timer timer)
        {
            _timer = timer;
        }

        public ThreadTimer(Action<object> callback)
        {
            _timer = new Timer(s => callback(s));
        }

        public ThreadTimer(Action<object> callback, object state, int dueTime, int period)
        {
            _timer = new Timer(s => callback(s), state, dueTime, period);
        }

        public ThreadTimer(Action<object> callback, object state, long dueTime, long period)
        {
            _timer = new Timer(s => callback(s), state, dueTime, period);
        }

        public ThreadTimer(Action<object> callback, object state, TimeSpan dueTime, TimeSpan period)
        {
            _timer = new Timer(s => callback(s), state, dueTime, period);
        }

        public void Change(uint dueTime, uint period)
        {
            _timer.Change(dueTime, period);
        }

        public void Change(TimeSpan dueTime, TimeSpan period)
        {
            _timer.Change(dueTime, period);
        }

        public void Change(long dueTime, long period)
        {
            _timer.Change(dueTime, period);
        }

        public void Change(int dueTime, int period)
        {
            _timer.Change(dueTime, period);
        }

        ~ThreadTimer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            _disposed = true;

            if (disposing)
            {
                _timer.Dispose();
            }
        }
    }
}
