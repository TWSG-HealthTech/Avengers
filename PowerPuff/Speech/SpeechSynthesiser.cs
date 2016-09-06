using System;
using Microsoft.Speech.Synthesis;
using PowerPuff.Common.Speech;

namespace PowerPuff.Speech
{
    public class SpeechSynthesiser : ISpeechSynthesiser, IDisposable
    {
        private SpeechSynthesizer _synthesizer = new SpeechSynthesizer();

        public SpeechSynthesiser()
        {
            _synthesizer.SetOutputToDefaultAudioDevice();
        }

        public void Speak(string speech)
        {
            Console.WriteLine($"Speaking: '{speech}'");
            _synthesizer.Speak(speech);
        }

        public void Dispose()
        {
            _synthesizer.Dispose();
        }
    }
}