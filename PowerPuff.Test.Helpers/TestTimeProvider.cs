using PowerPuff.Common.Helpers;
using System;

namespace PowerPuff.Test.Helpers
{
    public class TestTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get; set; } = DateTime.Now;
    }
}
