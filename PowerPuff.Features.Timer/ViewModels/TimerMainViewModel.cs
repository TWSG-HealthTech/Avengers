using System;
using Prism.Regions;
using Prism;
using System.Windows.Threading;
using Autofac;
using Prism.Mvvm;

namespace PowerPuff.Features.Timer.ViewModels
{
    public class TimerMainViewModel : BindableBase
    {
        private TimeSpan timer;
        private DispatcherTimer _timerObject;

        public TimerMainViewModel ()
        {
            timer = new TimeSpan(0,1,5);
            _timerObject = new DispatcherTimer();
            _timerObject.Interval = TimeSpan.FromSeconds(1);
            _timerObject.Tick += new EventHandler(timerTick);
            _timerObject.Start();
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

        private void timerTick(object obj, EventArgs e)
        {
            timer = timer.Subtract(new TimeSpan(0, 0, 1));
            Hours = string.Format($"{timer.Hours:D2}");
            Minutes = string.Format($"{timer.Minutes:D2}");
            Seconds = string.Format($"{timer.Seconds:D2}");

            if (timer.TotalSeconds == 0)
            {
                _timerObject.Stop();
            }
        }
    }
}
