using System;

namespace PowerPuff.Speech
{
    public interface IActiveListener : IDisposable
    {
        void BeginActiveListening();
        event Action ActiveListeningStarted;
        event Action ActiveListeningStopped;
    }
}