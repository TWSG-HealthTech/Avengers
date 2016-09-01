using Machine.Specifications;
using PowerPuff.Speech;
using M = Moq;

namespace PowerPuff.Tests.Speech
{
    [Subject(typeof(NoneIntentHandler))]
    public class NoneIntentHandlerTests
    {
        private static M.Mock<ISpeechSynthesiser> _speechSynthesiserMock;
        private static NoneIntentHandler _subject;

        Establish context = () =>
        {
            _speechSynthesiserMock = new M.Mock<ISpeechSynthesiser>();
            _subject = new NoneIntentHandler(_speechSynthesiserMock.Object);
        };

        class static_use
        {
            It has_the_None_intent_name = () => _subject.IntentName.ShouldEqual("None");
        }

        class Handle
        {
            private Because of = () => _subject.Handle();

            It says_that_it_does_not_know = () => _speechSynthesiserMock.Verify(ss => ss.Speak("I'm sorry, I don't know how to do that."));
        }
    }
}
