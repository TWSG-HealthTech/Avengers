using System;
using System.Globalization;
using Microsoft.Speech.Recognition;
using PowerPuff.Common.Settings;

namespace PowerPuff.Speech
{
    public class PassiveListener : IPassiveListener, IDisposable
    {
        private SpeechRecognitionEngine _sre;

        public event Action OnWakeWord;

        public PassiveListener(IApplicationSettings applicationSettings)
        {
            var ci = new CultureInfo(applicationSettings.Locale);
            _sre = new SpeechRecognitionEngine(ci);
            _sre.SpeechRecognized += OnSpeechRecognized;

            _sre.LoadGrammar(new Grammar(new GrammarBuilder("Hey Kate")));

            _sre.SetInputToDefaultAudioDevice();
        }

        private void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine($"Heard: '{e.Result.Text}', Confidence: {e.Result.Confidence}");
            if (e.Result.Confidence > 0.7) OnWakeWord?.Invoke();
        }

        public void StartListening()
        {
            Console.WriteLine("PassiveListener starting");
            _sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void StopListening()
        {
            Console.WriteLine("PassiveListener stopping");
            _sre.RecognizeAsyncCancel();
        }

        public void Dispose()
        {
            StopListening();
            _sre.Dispose();
        }
    }
}
