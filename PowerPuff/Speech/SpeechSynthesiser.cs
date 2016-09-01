using Microsoft.Speech.Synthesis;

namespace PowerPuff.Speech
{
    public class SpeechSynthesiser : ISpeechSynthesiser
    {
        private SpeechSynthesizer _synthesizer = new SpeechSynthesizer();

        public SpeechSynthesiser()
        {
            _synthesizer.SetOutputToDefaultAudioDevice();
            _synthesizer.Speak("I can speak... Hooray!");
        }

        public void Speak(string speech)
        {
            _synthesizer.Speak(speech);
        }
    }
}