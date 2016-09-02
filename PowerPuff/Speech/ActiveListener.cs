using Microsoft.ProjectOxford.SpeechRecognition;
using PowerPuff.Common.Settings;
using System;
using Microsoft.Speech.Recognition;

namespace PowerPuff.Speech
{
    internal class ActiveListener : IActiveListener
    {
        private MicrophoneRecognitionClient _microphoneClient;
        private readonly IApplicationSettings _applicationSettings;
        private readonly IIntentProcessor _intentProcessor;
        private readonly IPassiveListener _passiveListener;

        public ActiveListener(IApplicationSettings applicationSettings, IIntentProcessor intentProcessor, IPassiveListener passiveListener)
        {
            _applicationSettings = applicationSettings;
            _intentProcessor = intentProcessor;
            _passiveListener = passiveListener;
            SetupActiveMic();
            var sre = new SpeechRecognitionEngine();
            sre.SetInputToDefaultAudioDevice();
            _passiveListener.OnWakeWord += BeginActiveListening;
            _passiveListener.StartListening();
        }

        public void BeginActiveListening()
        {
//            SetupActiveMic();
            _passiveListener.StopListening();
            _microphoneClient.StartMicAndRecognition();
        }

        private void SetupActiveMic()
        {
            _microphoneClient?.Dispose();
            _microphoneClient = SpeechRecognitionServiceFactory.CreateMicrophoneClient(
                SpeechRecognitionMode.ShortPhrase, 
                _applicationSettings.Locale,
                _applicationSettings.SpeechPrimaryKey,
                _applicationSettings.SpeechSecondaryKey//,
//                _applicationSettings.LuisAppId,
//                _applicationSettings.LuisSubscriptionId
                );

            // Event handlers for speech recognition results
            _microphoneClient.OnMicrophoneStatus += OnMicrophoneStatus;
            _microphoneClient.OnPartialResponseReceived += OnPartialResponseReceivedHandler;
            _microphoneClient.OnResponseReceived += OnMicShortPhraseResponseReceivedHandler;
            _microphoneClient.OnConversationError += OnConversationErrorHandler;

            // Event handler for intent result
            _microphoneClient.OnIntent += OnIntentHandler;
        }

        private void OnConversationErrorHandler(object sender, SpeechErrorEventArgs e)
        {
            Console.WriteLine($"ActiveError: {e.SpeechErrorCode} - {e.SpeechErrorText}");
        }

        private void OnPartialResponseReceivedHandler(object sender, PartialSpeechResponseEventArgs e)
        {
            Console.WriteLine($"Partial: {e.PartialResult}");
        }

        private void OnMicShortPhraseResponseReceivedHandler(object sender, SpeechResponseEventArgs e)
        {
            Console.WriteLine(e.PhraseResponse.Results.ToString());
        }

        private void OnIntentHandler(object sender, SpeechIntentEventArgs e)
        {
            _intentProcessor.Process("None");
        }

        public event Action ActiveListeningStarted;
        public event Action ActiveListeningStopped;

        private void OnMicrophoneStatus(object sender, MicrophoneEventArgs e)
        {
            if (e.Recording)
            {
                Console.WriteLine("ActiveListener starting");
                ActiveListeningStarted?.Invoke();
            }
            else
            {
                Console.WriteLine("ActiveListener stopping");
                ActiveListeningStopped?.Invoke();
                _passiveListener.StartListening();
            }
        }

        public void Dispose()
        {
            _microphoneClient?.Dispose();
        }
    }
}