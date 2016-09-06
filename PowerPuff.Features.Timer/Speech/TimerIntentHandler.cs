using Microsoft.Cognitive.LUIS;
using PowerPuff.Common.Speech;
using PowerPuff.Features.Timer.Navigation;
using System.Threading.Tasks;

namespace PowerPuff.Features.Timer.Speech
{
    public class TimerIntentHandler : IIntentHandler
    {
        private readonly ISpeechSynthesiser _speechSynthesiser;
        private readonly TimerNavigator _timerNavigator;

        public TimerIntentHandler(ISpeechSynthesiser speechSynthesiser, TimerNavigator timerNavigator)
        {
            _speechSynthesiser = speechSynthesiser;
            _timerNavigator = timerNavigator;
        }

        [IntentHandler(0, Name = "SetTimer")]
        public async Task<bool> SetTimer(LuisResult result, object context)
        {
            _timerNavigator.GoToTimerPage();
            _speechSynthesiser.Speak("Setting timer");
            return true;
        }
    }
}
