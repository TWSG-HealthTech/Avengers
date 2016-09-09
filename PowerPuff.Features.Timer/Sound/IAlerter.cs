using System.Media;

namespace PowerPuff.Features.Timer.Sound
{
    public class Alerter : IAlerter
    {
        public void Alert()
        {
            var player = new SoundPlayer(Properties.Resources.timesup);
            player.Play();
        }
    }
    public interface IAlerter
    {
        void Alert();
    }
}
