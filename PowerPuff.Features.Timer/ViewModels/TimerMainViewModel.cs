using System;
using System.Windows;
using System.Windows.Threading;
using Prism.Mvvm;
using Prism.Commands;

namespace PowerPuff.Features.Timer.ViewModels
{
    public class TimerMainViewModel : BindableBase
    {
        private TimeSpan timer;
        private DispatcherTimer _timerObject;

        private bool _isTimerEnabled;
        public bool IsTimerEnabled
        {
            get { return _isTimerEnabled; }
            set { SetProperty(ref _isTimerEnabled, value); }
        }

        public TimerMainViewModel ()
        {
            _timerObject = new DispatcherTimer();
            _timerObject.Interval = TimeSpan.FromSeconds(1);
            _timerObject.Tick += new EventHandler(timerTick);

            timer = new TimeSpan();
            UpdatePropertiesForTimerDisplay();

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
            timer = timer.Add(new TimeSpan(0, 0, 1));
            UpdatePropertiesForTimerDisplay();
        }

        public DelegateCommand SubtractSecondsButton { get; private set; }

        private void DecreaseSecond()
        {
            if (timer.TotalSeconds > 0)
            {
                timer = timer.Subtract(new TimeSpan(0, 0, 1));
            }
            UpdatePropertiesForTimerDisplay();
        }

        public DelegateCommand AddMinutesButton { get; set; }

        private void IncreaseMinute()
        {
            timer = timer.Add(new TimeSpan(0, 1, 0));
            UpdatePropertiesForTimerDisplay();
        }

        public DelegateCommand SubtractMinutesButton { get; set; }

        private void DecreaseMinute()
        {
            if (timer.Minutes > 0)
            {
                timer = timer.Subtract(new TimeSpan(0, 1, 0));
            }
            UpdatePropertiesForTimerDisplay();
        }

        public DelegateCommand AddHoursButton { get; set; }

        private void IncreaseHour()
        {
            timer = timer.Add(new TimeSpan(1, 0, 0));
            UpdatePropertiesForTimerDisplay();
        }

        public DelegateCommand SubtractHoursButton { get; set; }
        public object StartTimerButtonVisibility { get; private set; }

        private void DecreaseHour()
        {
            if (timer.Hours > 0)
            {
                timer = timer.Subtract(new TimeSpan(1, 0, 0));
            }
            UpdatePropertiesForTimerDisplay();
        }

        private void timerTick(object obj, EventArgs e)
        {
            DecreaseSecond();
            if (timer.TotalSeconds == 0)
            {
                StopTimer();
            }
        }

        private void UpdatePropertiesForTimerDisplay()
        {
            Hours = $"{timer.Hours:D2}";
            Minutes = $"{timer.Minutes:D2}";
            Seconds = $"{timer.Seconds:D2}";
        }
    }
}
