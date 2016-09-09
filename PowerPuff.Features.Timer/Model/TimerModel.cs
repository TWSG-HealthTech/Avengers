using System;

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
                _timer.Stop();
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
        public virtual event Action<TimeSpan> OnStarted;
        public virtual event Action<TimeSpan> OnStopped;
        public virtual event Action<TimeSpan> OnTick;
        public virtual event Action OnCompleted;
        public virtual event Action<TimeSpan> OnReset;

        public virtual void Start()
        {
            if (_duration <= TimeSpan.Zero) return;
            _timer.Start();
            OnStarted?.Invoke(_duration);
        }

        public virtual void Stop()
        {
            var elapsed = _timer.Stop();
            OnStopped?.Invoke(_duration - elapsed);  
        }

        public virtual void Reset()
        {
            _timer.Reset();
            OnReset?.Invoke(_duration);   
        }

    }

    public class Timer : ITimer
    {
        public event Action<TimeSpan> OnTick;

        public void Start()
        {

        }

        public TimeSpan Stop()
        {
            return TimeSpan.Zero;
        }

        public void Reset()
        {
            
        }
    }

    public interface ITimer
    {
        void Start();
        TimeSpan Stop();
        event Action<TimeSpan> OnTick;
        void Reset();
    }
}
