using System.IO;

namespace PowerPuff.Common.Helpers
{
    public interface ISoundPlayer
    {
        void Play(Stream sound);
    }
}