using Machine.Specifications;
using Microsoft.Cognitive.LUIS;
using Newtonsoft.Json.Linq;
using PowerPuff.Common;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Navigation;
using PowerPuff.Common.Speech;
using PowerPuff.Features.Timer.Speech;
using System;
using M = Moq;

namespace PowerPuff.Features.Timer.Tests.Speech
{
    [Subject(typeof(TimerIntentHandler))]
    class TimerIntentHandlerTests
    {
        protected static TimerIntentHandler _subject;
        protected static Model.Timer _timer;
        protected static M.Mock<ISpeechSynthesiser> _speechSynthesiserMock;
        protected static M.Mock<INavigator> _navigatorMock;

        protected static LuisResult _luisResult;
        protected static bool? _handled;

        Establish baseContext = () =>
        {
            _timer = new Model.Timer();
            _navigatorMock = new M.Mock<INavigator>();
            _speechSynthesiserMock = new M.Mock<ISpeechSynthesiser>();
            _subject = new TimerIntentHandler(_speechSynthesiserMock.Object, _navigatorMock.Object, _timer);
        };

        class Successful_Request : TimerIntentHandlerTests
        {

            Establish context = () =>
            {
                _luisResult = new LuisResult();
                _luisResult.Load(JToken.Parse(@"
{
  ""query"": ""set a 2 hour timer"",
  ""intents"": [
    {
                ""intent"": ""SetTimer"",
      ""score"": 0.844260931,
      ""actions"": [
        {
          ""triggered"": true,
          ""name"": ""SetTimer"",
          ""parameters"": [
            {
              ""name"": ""duration"",
              ""required"": true,
              ""value"": [
                {
                  ""entity"": ""2 hour"",
                  ""type"": ""builtin.datetime.duration"",
                  ""resolution"": {
                    ""duration"": ""PT2H""
                  }
    }
              ]
            }
          ]
        }
      ]
    },
    {
      ""intent"": ""None"",
      ""score"": 0.0204212964
    }
  ],
  ""entities"": [
    {
      ""entity"": ""2 hour"",
      ""type"": ""builtin.datetime.duration"",
      ""startIndex"": 6,
      ""endIndex"": 11,
      ""resolution"": {
        ""duration"": ""PT2H""
      }
    }
  ]
}
"));
            };

            private Because of = async () => _handled = await _subject.SetTimer(_luisResult, null);

            It navigates_to_timer_view = () => _navigatorMock.Verify(n => n.GoToPage(NavigableViews.Timer.MainView.GetFullName()));

            It handles_the_intent = () => _handled.ShouldEqual(true);

            It responds_with_speech = () => _speechSynthesiserMock.Verify(ss => ss.Speak("Setting timer for 2 hours"));

            It updated_the_timer = () => _timer.Duration.ShouldEqual(new TimeSpan(2, 0, 0));
        }

        class Incomplete_Request
        {
            private Establish context = () =>
            {
                _luisResult = new LuisResult();
                _luisResult.Load(JToken.Parse(@"
{
  ""query"": ""set a 2 hour timer"",
  ""intents"": [
    {
      ""intent"": ""SetTimer"",
      ""score"": 0.844260931,
      ""actions"": [
        {
          ""triggered"": false,
          ""name"": ""SetTimer"",
          ""parameters"": [
            {
              ""name"": ""duration"",
              ""required"": true,
              ""value"": null
            }
          ]
        }
      ]
    },
    {
      ""intent"": ""None"",
      ""score"": 0.0204212964
    }
  ],
  ""entities"": [
    {
      ""entity"": ""2 hour"",
      ""type"": ""builtin.datetime.duration"",
      ""startIndex"": 6,
      ""endIndex"": 11,
      ""resolution"": {
        ""duration"": ""PT2H""
      }
    }
  ]
}
"));
            };

            private Because of = async () => _handled = await _subject.SetTimer(_luisResult, null);

            It does_not_handle_the_intent = () => _handled.ShouldEqual(false);
        }

        class Invalid_Duration
        {
            private Establish context = () =>
            {
                _luisResult = new LuisResult();
                _luisResult.Load(JToken.Parse(@"
{
  ""query"": ""set a 2 hour timer"",
  ""intents"": [
    {
                ""intent"": ""SetTimer"",
      ""score"": 0.844260931,
      ""actions"": [
        {
          ""triggered"": true,
          ""name"": ""SetTimer"",
          ""parameters"": [
            {
              ""name"": ""duration"",
              ""required"": true,
              ""value"": [
                {
                  ""entity"": ""tomorrow"",
                  ""type"": ""builtin.datetime.date"",
                  ""resolution"": {
                    ""date"": ""2016-09-08""
                  }
                }
              ]
            }
          ]
        }
      ]
    },
    {
      ""intent"": ""None"",
      ""score"": 0.0204212964
    }
  ],
  ""entities"": [
    {
     ""entity"": ""tomorrow"",
      ""type"": ""builtin.datetime.date"",
      ""startIndex"": 6,
      ""endIndex"": 13,
      ""resolution"": {
          ""date"": ""2016-09-08""
      }
    }
  ]
}
"));
            };

            private Because of = async () => _handled = await _subject.SetTimer(_luisResult, null);

            It does_not_handle_the_intent = () => _handled.ShouldEqual(false);
        }
    }
}
