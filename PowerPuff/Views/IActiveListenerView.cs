using System;

namespace PowerPuff.Views
{
    public interface IActiveListenerView
    {
        event Action OnListnerActivatorClick;

        void ActiveListeningStarted();
    }
}