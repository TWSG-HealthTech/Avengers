using System;

namespace PowerPuff.Features.Timer.Model
{
    public class Timer
    {
        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                if (value < TimeSpan.Zero) return;
                _duration = value;
                OnTimeSpanChanged?.Invoke(_duration);
            }
        }

        public event Action<TimeSpan> OnTimeSpanChanged;
    }
}
