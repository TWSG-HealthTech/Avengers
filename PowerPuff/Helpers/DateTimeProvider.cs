using PowerPuff.Common.Helpers;
using System;

namespace PowerPuff.Helpers
{
    class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
