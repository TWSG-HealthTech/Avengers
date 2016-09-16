using PowerPuff.Common.Helpers;
using System.IO;

namespace PowerPuff.Helpers
{
    class SoundPlayer : ISoundPlayer
    {
        public void Play(Stream sound)
        {
           var player = new System.Media.SoundPlayer(sound);
           player.Play();
        }
    }
}
