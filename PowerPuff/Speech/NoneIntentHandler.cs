namespace PowerPuff.Speech
{
    public class NoneIntentHandler : IIntentHandler
    {
        private readonly ISpeechSynthesiser _speechSynthesiser;

        public NoneIntentHandler(ISpeechSynthesiser speechSynthesiser)
        {
            _speechSynthesiser = speechSynthesiser;
        }

        public void Handle()
        {
            _speechSynthesiser.Speak("I'm sorry, I don't know how to do that.");
        }
    }
}
