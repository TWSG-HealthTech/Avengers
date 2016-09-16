using PowerPuff.Common.Helpers;

namespace PowerPuff.Features.Timer.Sound
{
    public class Alerter : IAlerter
    {
        private readonly ISoundPlayer _soundPlayer;

        public Alerter(ISoundPlayer soundPlayer)
        {
            _soundPlayer = soundPlayer;
        }

        public void Alert()
        {
            _soundPlayer.Play(Properties.Resources.timesup);
        }
    }
    public interface IAlerter
    {
        void Alert();
    }
}
