using System;
using System.Windows.Threading;
using Prism.Mvvm;
using Prism.Commands;

namespace PowerPuff.Features.Timer.ViewModels
{
    public class TimerMainViewModel : BindableBase
    {
        private TimeSpan timer;
        private DispatcherTimer _timerObject;

        public TimerMainViewModel ()
        {
            _timerObject = new DispatcherTimer();
            _timerObject.Interval = TimeSpan.FromSeconds(1);
            _timerObject.Tick += new EventHandler(timerTick);

            timer = new TimeSpan();
            UpdatePropertiesForTimerDisplay();

            IsTimerEnabled = _timerObject.IsEnabled.ToString();

            ButtonStartTimer = new DelegateCommand(StartTimer);
            ButtonStopTimer = new DelegateCommand(StopTimer);
        }

        public DelegateCommand ButtonStartTimer { get; private set; }

        public void StartTimer()
        {
            timer = new TimeSpan(0, 1, 5);
            UpdatePropertiesForTimerDisplay();

            _timerObject.Start();
            IsTimerEnabled = _timerObject.IsEnabled.ToString();
        }

        public DelegateCommand ButtonStopTimer { get; private set; }

        public void StopTimer()
        {
            _timerObject.Stop();
            timer = new TimeSpan(0, 1, 5);
            UpdatePropertiesForTimerDisplay();
            IsTimerEnabled = _timerObject.IsEnabled.ToString();
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

        private string _isTimerEnabled;
        public string IsTimerEnabled
        {
            get { return _isTimerEnabled.ToString(); }
            set { SetProperty(ref _isTimerEnabled, value); }
        }

        private void timerTick(object obj, EventArgs e)
        {
            timer = timer.Subtract(new TimeSpan(0, 0, 1));
            UpdatePropertiesForTimerDisplay();

            if (timer.TotalSeconds == 0)
            {
                StopTimer();
            }
        }

        private void UpdatePropertiesForTimerDisplay()
        {
            Hours = string.Format($"{timer.Hours:D2}");
            Minutes = string.Format($"{timer.Minutes:D2}");
            Seconds = string.Format($"{timer.Seconds:D2}");
        }
    }
}
