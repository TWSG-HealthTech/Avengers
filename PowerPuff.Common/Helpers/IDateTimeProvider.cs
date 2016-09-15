using System;

namespace PowerPuff.Common.Helpers
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
