using Microsoft.ProjectOxford.SpeechRecognition;
using PowerPuff.Common.Settings;
using System;
using Microsoft.Speech.Recognition;

namespace PowerPuff.Speech
{
    internal class ActiveListener : IActiveListener
    {
        private MicrophoneRecognitionClientWithIntent _microphoneClient;
        private IApplicationSettings _applicationSettings;
        private readonly IIntentProcessor _intentProcessor;

        public ActiveListener(IApplicationSettings applicationSettings, IIntentProcessor intentProcessor)
        {
            _applicationSettings = applicationSettings;
            _intentProcessor = intentProcessor;
            SetupActiveMic();
            var sre = new SpeechRecognitionEngine();
            sre.SetInputToDefaultAudioDevice();
        }

        public void BeginActiveListening()
        {
//            SetupActiveMic();

            _microphoneClient.StartMicAndRecognition();
        }

        private void SetupActiveMic()
        {
            _microphoneClient?.Dispose();
            _microphoneClient = SpeechRecognitionServiceFactory.CreateMicrophoneClientWithIntent(
                _applicationSettings.Locale,
                _applicationSettings.SpeechPrimaryKey,
                _applicationSettings.SpeechSecondaryKey,
                _applicationSettings.LuisAppId,
                _applicationSettings.LuisSubscriptionId
                );

            // Event handlers for speech recognition results
            _microphoneClient.OnMicrophoneStatus += OnMicrophoneStatus;
            //            _microphoneClient.OnPartialResponseReceived += OnPartialResponseReceivedHandler;
            _microphoneClient.OnResponseReceived += OnMicShortPhraseResponseReceivedHandler;
            //            _microphoneClient.OnConversationError += OnConversationErrorHandler;

            // Event handler for intent result
            _microphoneClient.OnIntent += OnIntentHandler;
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
                ActiveListeningStarted?.Invoke();
            }
            else
            {
                ActiveListeningStopped?.Invoke();
            }
        }

        public void Dispose()
        {
            _microphoneClient?.Dispose();
        }
    }
}