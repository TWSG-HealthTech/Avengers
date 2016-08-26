using System;

namespace PowerPuff.Common.Helpers
{
    public static class EnumExtensions
    {
        public static string GetFullName(this Enum e)
        {
            return $"{e.GetType().FullName}.{e}";
        }
    }
}
