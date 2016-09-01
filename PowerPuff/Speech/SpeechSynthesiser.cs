using Microsoft.Speech.Synthesis;

namespace PowerPuff.Speech
{
    public class SpeechSynthesiser : ISpeechSynthesiser
    {
        private SpeechSynthesizer _synthesizer = new SpeechSynthesizer();

        public SpeechSynthesiser()
        {
            _synthesizer.SetOutputToDefaultAudioDevice();
        }

        public void Speak(string speech)
        {
            _synthesizer.Speak(speech);
        }
    }
}