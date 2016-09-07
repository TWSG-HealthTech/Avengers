using Machine.Specifications;
using Microsoft.Cognitive.LUIS;
using Newtonsoft.Json.Linq;
using PowerPuff.Common.Speech;
using PowerPuff.Features.Timer.Navigation;
using PowerPuff.Features.Timer.Speech;
using PowerPuff.Features.Timer.Tests.Navigation;
using PowerPuff.Test.Helpers;
using Prism.Regions;
using M = Moq;

namespace PowerPuff.Features.Timer.Tests.Speech
{
    [Subject(typeof(TimerIntentHandler))]
    class TimerIntentHandlerTests
    {
        protected static TimerIntentHandler _subject;
        protected static M.Mock<ISpeechSynthesiser> _speechSynthesiserMock;
        protected static M.Mock<IRegionManager> _regionManagerMock;

        protected static LuisResult _luisResult;
        protected static bool? _handled;

        Establish context = () =>
        {
            _regionManagerMock = new M.Mock<IRegionManager>();
            var timerNavigator = new TimerNavigator(_regionManagerMock.Object, new TestDispatcher());
            _speechSynthesiserMock = new M.Mock<ISpeechSynthesiser>();
            _subject = new TimerIntentHandler(_speechSynthesiserMock.Object, timerNavigator);
        };

        class Successful_Request : TimerIntentHandlerTests
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

            Behaves_like<TimerNavigatorBehaviors> it_navigates_to_timer_view;

            It handles_the_intent = () => _handled.ShouldEqual(true);

            It responds_with_speech = () => _speechSynthesiserMock.Verify(ss => ss.Speak("Setting timer for 2 hours"));
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
