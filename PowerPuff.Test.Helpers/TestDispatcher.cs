using System;
using PowerPuff.Common.Helpers;

namespace PowerPuff.Test.Helpers
{
    public class TestDispatcher : IDispatcher
    {
        public void Invoke(Action callback)
        {
            callback.Invoke();
        }
    }
}
