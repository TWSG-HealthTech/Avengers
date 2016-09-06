using System;

namespace PowerPuff.Common.Helpers
{
    public interface IDispatcher
    {
        void Invoke(Action callback);
    }
}
