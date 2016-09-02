using System;

namespace PowerPuff.Speech
{
    interface IPassiveListener
    {
        void StartListening();
        void StopListening();
        event Action OnWakeWord;
    }
}
