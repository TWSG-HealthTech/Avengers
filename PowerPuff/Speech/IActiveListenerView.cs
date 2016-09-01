using System;

namespace PowerPuff.Speech
{
    public interface IActiveListenerView
    {
        event Action OnListnerActivatorClick;

        void ActiveListeningStarted();
        void ActiveListeningStopped();
    }
}