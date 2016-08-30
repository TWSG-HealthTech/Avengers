using System;

namespace PowerPuff.Speech
{
    public interface IActiveListener
    {
        void BeginActiveListening();
        event Action ActiveListeningStarted;
        event Action ActiveListeningStopped;
    }
}