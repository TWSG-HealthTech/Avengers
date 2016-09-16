﻿using Autofac;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Settings;
using PowerPuff.Features.MotionDetection.Models;
using PowerPuff.Features.MotionDetection.Services;
using PowerPuff.Features.MotionDetection.ViewModels;
using PowerPuff.Features.MotionDetection.Views;
using Prism.Autofac;
using Prism.Modularity;

namespace PowerPuff.Features.MotionDetection
{
    public class MotionDetectionModule : IModule
    {
        private readonly IContainer _container;

        public MotionDetectionModule(IContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            ConfigureDependencies();
            ConfigureSettingMenu();

            _container.Resolve<Alerter>();
        }

        private void ConfigureDependencies()
        {
            var updater = new ContainerBuilder();

            updater.RegisterType<MotionDetectionModel>().As<IMotionDetectionModel>().SingleInstance();
            updater.RegisterType<SettingsViewModel>();
            updater.RegisterType<MotionDetector>().As<IMotionDetector>().SingleInstance();
            updater.RegisterType<Timer>().As<ITimer>().SingleInstance();
            updater.RegisterType<Alerter>().SingleInstance();

            updater.RegisterTypeForNavigation<SettingsView>(NavigableViews.MotionDetection.SettingsView.GetFullName());

            updater.Update(_container);
        }

        private void ConfigureSettingMenu()
        {
            var settingsRepository = _container.Resolve<IMenuSettingsRepository>();
            settingsRepository.RegisterMenu("Motion Detection", NavigableViews.MotionDetection.SettingsView.GetFullName());
        }
    }
}
