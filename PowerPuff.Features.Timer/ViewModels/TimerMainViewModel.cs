﻿using PowerPuff.Features.Timer.Model;
using PowerPuff.Features.Timer.Sound;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace PowerPuff.Features.Timer.ViewModels
{
    public class TimerMainViewModel : BindableBase
    {
        private TimerModel _timerModel;

        private bool _isModifiable = true;
        public bool IsModifiable
        {
            get { return _isModifiable; }
            set { SetProperty(ref _isModifiable, value); }
        }

        private bool _isStartable = true;
        public bool IsStartable
        {
            get { return _isStartable; }
            set { SetProperty(ref _isStartable, value); }
        }

        public TimerMainViewModel (TimerModel timerModel, IAlerter alerter)
        {
            _timerModel = timerModel;
            _timerModel.OnDurationChanged += UpdatePropertiesForTimerDisplay;
            _timerModel.OnStarted += () =>
            {
                IsModifiable = false;
                IsStartable = false;
            };
            _timerModel.OnPaused += timeRemaining =>
            {
                UpdatePropertiesForTimerDisplay(timeRemaining);
                IsStartable = true;
                IsModifiable = false;
            };
            _timerModel.OnTick += UpdatePropertiesForTimerDisplay;

            _timerModel.OnReset += duration =>
            {
                UpdatePropertiesForTimerDisplay(duration);
                IsStartable = true;
                IsModifiable = true;
            };

            _timerModel.OnCompleted += () =>
            {
                UpdatePropertiesForTimerDisplay(TimeSpan.Zero);
                IsModifiable = false;
                IsStartable = false;
                alerter.Alert();
            };

            UpdatePropertiesForTimerDisplay(_timerModel.Duration);

            StartTimerButton = new DelegateCommand(_timerModel.Start);
            PauseTimerButton = new DelegateCommand(_timerModel.Pause);
            ResetTimerButton = new DelegateCommand(_timerModel.Reset);
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

        public DelegateCommand PauseTimerButton { get; private set; }

        public DelegateCommand AddSecondsButton { get; private set; }

        private void IncreaseSecond()
        {
            _timerModel.Duration = _timerModel.Duration.Add(new TimeSpan(0, 0, 1));
        }

        public DelegateCommand SubtractSecondsButton { get; private set; }

        private void DecreaseSecond()
        {
            _timerModel.Duration = _timerModel.Duration.Subtract(new TimeSpan(0, 0, 1));
        }

        public DelegateCommand AddMinutesButton { get; set; }

        private void IncreaseMinute()
        {
            _timerModel.Duration = _timerModel.Duration.Add(new TimeSpan(0, 1, 0));
        }

        public DelegateCommand SubtractMinutesButton { get; set; }

        private void DecreaseMinute()
        {
            _timerModel.Duration = _timerModel.Duration.Subtract(new TimeSpan(0, 1, 0));
        }

        public DelegateCommand AddHoursButton { get; set; }

        private void IncreaseHour()
        {
            _timerModel.Duration = _timerModel.Duration.Add(new TimeSpan(1, 0, 0));
        }

        public DelegateCommand SubtractHoursButton { get; set; }
        public object StartTimerButtonVisibility { get; private set; }

        private void DecreaseHour()
        {
            _timerModel.Duration = _timerModel.Duration.Subtract(new TimeSpan(1, 0, 0));
        }

        public DelegateCommand ResetTimerButton { get; set; }

        private void UpdatePropertiesForTimerDisplay(TimeSpan timeSpan)
        {
            Hours = $"{timeSpan.Hours:D2}";
            Minutes = $"{timeSpan.Minutes:D2}";
            Seconds = $"{timeSpan.Seconds:D2}";
        }
    }
}
