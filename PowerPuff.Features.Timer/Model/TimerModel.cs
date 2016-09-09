using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace PowerPuff.Features.Timer.Model
{
    public class TimerModel
    {
        private readonly ITimer _timer;

        public TimerModel(ITimer timer)
        {
            _timer = timer;
            _timer.OnTick += _timer_OnTick;
        }

        private void _timer_OnTick(TimeSpan elapsed)
        {
            var timeRemaining = _duration - elapsed;
            if (timeRemaining <= TimeSpan.Zero)
            {
                OnCompleted?.Invoke();
                _timer.Pause();
                return;
            }
            OnTick?.Invoke(timeRemaining);
        }

        private TimeSpan _duration;
        public virtual TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                if (value < TimeSpan.Zero) return;
                _duration = value;
                OnDurationChanged?.Invoke(_duration);
            }
        }

        public virtual event Action<TimeSpan> OnDurationChanged;
        public virtual event Action OnStarted;
        public virtual event Action<TimeSpan> OnPaused;
        public virtual event Action<TimeSpan> OnTick;
        public virtual event Action OnCompleted;
        public virtual event Action<TimeSpan> OnReset;

        public virtual void Start()
        {
            if (_duration <= TimeSpan.Zero) return;
            _timer.Start();
            OnStarted?.Invoke();
        }

        public virtual void Pause()
        {
            var elapsed = _timer.Pause();
            OnPaused?.Invoke(_duration - elapsed);  
        }

        public virtual void Reset()
        {
            _timer.Reset();
            OnReset?.Invoke(_duration);   
        }

    }

    public class Timer : ITimer
    {
        private Stopwatch _stopWatch = new Stopwatch();
        private System.Threading.Timer _timer;

        public event Action<TimeSpan> OnTick;

        public void Start()
        {
            if (_stopWatch.IsRunning) return;
            _stopWatch.Start();
            _timer = new System.Threading.Timer(state =>
            {
                OnTick?.Invoke(_stopWatch.Elapsed);
            }, null, TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(50));
            
        }

        public TimeSpan Pause()
        {
            _timer?.Dispose();
            _timer = null;
            _stopWatch.Stop();
            return _stopWatch.Elapsed;
        }

        public void Reset()
        {
            _timer?.Dispose();
            _timer = null;
            _stopWatch.Reset();
        }
    }

    public interface ITimer
    {
        void Start();
        TimeSpan Pause();
        event Action<TimeSpan> OnTick;
        void Reset();
    }
}
