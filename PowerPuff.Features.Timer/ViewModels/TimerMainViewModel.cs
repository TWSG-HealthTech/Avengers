using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Media;
using System.Windows.Threading;

namespace PowerPuff.Features.Timer.ViewModels
{
    public class TimerMainViewModel : BindableBase
    {
        private DispatcherTimer _timerObject;
        private Model.Timer _timer;

        private bool _isTimerEnabled;
        public bool IsTimerEnabled
        {
            get { return _isTimerEnabled; }
            set { SetProperty(ref _isTimerEnabled, value); }
        }

        public TimerMainViewModel (Model.Timer timer)
        {
            _timerObject = new DispatcherTimer();
            _timerObject.Interval = TimeSpan.FromSeconds(1);
            _timerObject.Tick += new EventHandler(timerTick);

            _timer = timer;
            _timer.OnTimeSpanChanged += UpdatePropertiesForTimerDisplay;
            UpdatePropertiesForTimerDisplay(_timer.Duration);

            IsTimerEnabled = _timerObject.IsEnabled;

            StartTimerButton = new DelegateCommand(StartTimer);
            StopTimerButton = new DelegateCommand(StopTimer);
            AddSecondsButton = new DelegateCommand(IncreaseSecond);
            SubtractSecondsButton = new DelegateCommand(DecreaseSecond);
            AddMinutesButton = new DelegateCommand(IncreaseMinute);
            SubtractMinutesButton = new DelegateCommand(DecreaseMinute);
            AddHoursButton = new DelegateCommand(IncreaseHour);
            SubtractHoursButton = new DelegateCommand(DecreaseHour);
        }

        private string _hours;
        public string Hours
        {
            get { return _hours; }
            set { SetProperty(ref _hours, value); }
        }

        private string _minutes;
        public string Minutes
        {
            get { return _minutes; }
            set { SetProperty(ref _minutes, value); }
        }

        private string _seconds;
        public string Seconds
        {
            get { return _seconds; }
            set { SetProperty(ref _seconds, value); }
        }

       
        public DelegateCommand StartTimerButton { get; private set; }

        private void StartTimer()
        {
            _timerObject.Start();
            IsTimerEnabled = _timerObject.IsEnabled;
        }

        public DelegateCommand StopTimerButton { get; private set; }

        private void StopTimer()
        {
            _timerObject.Stop();
            IsTimerEnabled = _timerObject.IsEnabled;
        }

        public DelegateCommand AddSecondsButton { get; private set; }

        private void IncreaseSecond()
        {
            _timer.Duration = _timer.Duration.Add(new TimeSpan(0, 0, 1));
        }

        public DelegateCommand SubtractSecondsButton { get; private set; }

        private void DecreaseSecond()
        {
            _timer.Duration = _timer.Duration.Subtract(new TimeSpan(0, 0, 1));
        }

        public DelegateCommand AddMinutesButton { get; set; }

        private void IncreaseMinute()
        {
            _timer.Duration = _timer.Duration.Add(new TimeSpan(0, 1, 0));
        }

        public DelegateCommand SubtractMinutesButton { get; set; }

        private void DecreaseMinute()
        {
            _timer.Duration = _timer.Duration.Subtract(new TimeSpan(0, 1, 0));
        }

        public DelegateCommand AddHoursButton { get; set; }

        private void IncreaseHour()
        {
            _timer.Duration = _timer.Duration.Add(new TimeSpan(1, 0, 0));
        }

        public DelegateCommand SubtractHoursButton { get; set; }
        public object StartTimerButtonVisibility { get; private set; }

        private void DecreaseHour()
        {
            _timer.Duration = _timer.Duration.Subtract(new TimeSpan(1, 0, 0));
        }

        private void timerTick(object obj, EventArgs e)
        {
            DecreaseSecond();
            if (_timer.Duration <= TimeSpan.Zero)
            {
                SoundPlayer player = new SoundPlayer(Properties.Resources.timesup);
                player.Play(); 
                StopTimer();
            }
        }

        private void UpdatePropertiesForTimerDisplay(TimeSpan timeSpan)
        {
            Hours = $"{timeSpan.Hours:D2}";
            Minutes = $"{timeSpan.Minutes:D2}";
            Seconds = $"{timeSpan.Seconds:D2}";
        }
    }
}
