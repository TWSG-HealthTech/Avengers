using Microsoft.Cognitive.LUIS;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Navigation;
using PowerPuff.Common.Speech;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PowerPuff.Features.Timer.Speech
{
    public class TimerIntentHandler : IIntentHandler
    {
        private readonly ISpeechSynthesiser _speechSynthesiser;
        private readonly INavigator _navigator;
        private readonly Model.Timer _timer;

        public TimerIntentHandler(ISpeechSynthesiser speechSynthesiser, INavigator navigator, Model.Timer timer)
        {
            _speechSynthesiser = speechSynthesiser;
            _navigator = navigator;
            _timer = timer;
        }

        [IntentHandler(0, Name = "SetTimer")]
        public async Task<bool> SetTimer(LuisResult result, object context)
        {
            var resolution = result.Intents.First(i => i.Name == "SetTimer")?
                .Actions.FirstOrDefault(x => x.Name == "SetTimer" && x.Triggered)?
                .Parameters.FirstOrDefault(x => x.Name == "duration")?
                .ParameterValues.FirstOrDefault(p => p.Type == "builtin.datetime.duration")?
                .Resolution["duration"] as string;
            if (resolution == null) return false;

            var duration = System.Xml.XmlConvert.ToTimeSpan(resolution);

            _timer.Duration = duration;

            _navigator.GoToPage(NavigableViews.Timer.MainView.GetFullName());
            _speechSynthesiser.Speak($"Setting timer for{Speechify(duration)}");
            return true;
        }

        private string Speechify(TimeSpan duration)
        {
            var speech = string.Empty;
            speech += Speechify(duration.Hours, "hour");
            speech += Speechify(duration.Minutes, "minute");
            speech += Speechify(duration.Seconds, "second");
            return speech;
        }

        private string Speechify(int quantity, string unit)
        {
            return quantity > 0 ? $" {quantity} {unit}{s(quantity)}" : string.Empty;
        }

        private string s(int value)
        {
            return value == 1 ? string.Empty : "s";
        }
    }
}
