using Machine.Specifications;
using PowerPuff.Speech;
using M = Moq;

namespace PowerPuff.Tests.Speech
{
    [Subject(typeof(IntentProcessor))]
    public class IntentProcessorTests
    {
        private static M.Mock<IIntentHandler> _intentHandlerA;
        private static M.Mock<IIntentHandler> _intentHandlerB;
        private static M.Mock<IIntentHandler> _defaultIntentHandler;

        private static IntentProcessor _subject;

        Establish context = () =>
        {
            _intentHandlerA = new M.Mock<IIntentHandler>();
            _intentHandlerA.SetupGet(ih => ih.IntentName).Returns("A");
            _intentHandlerB = new M.Mock<IIntentHandler>();
            _intentHandlerB.SetupGet(ih => ih.IntentName).Returns("B");
            _defaultIntentHandler = new M.Mock<IIntentHandler>();
            _defaultIntentHandler.SetupGet(ih => ih.IntentName).Returns("None");

            _subject = new IntentProcessor(new[] {_intentHandlerA.Object, _intentHandlerB.Object}, _defaultIntentHandler.Object);
        };

        public class intentA
        {
            Because of = () => _subject.Process("A");

            It calls_intent_handler_A = () => _intentHandlerA.Verify(ih => ih.Handle());
            It does_not_call_intent_handler_B = () => _intentHandlerB.Verify(ih => ih.Handle(), M.Times.Never);
            It does_not_call_default_handler = () => _defaultIntentHandler.Verify(ih => ih.Handle(), M.Times.Never);
        }
        public class intentB
        {
            Because of = () => _subject.Process("B");

            It calls_intent_handler_B = () => _intentHandlerB.Verify(ih => ih.Handle());
            It does_not_call_intent_handler_A = () => _intentHandlerA.Verify(ih => ih.Handle(), M.Times.Never);
            It does_not_call_default_handler = () => _defaultIntentHandler.Verify(ih => ih.Handle(), M.Times.Never);
        }

        public class unknown_intent
        {
            Because of = () => _subject.Process("Not an intent");

            It calls_default_handler = () => _defaultIntentHandler.Verify(ih => ih.Handle());
            It does_not_call_intent_handler_A = () => _intentHandlerA.Verify(ih => ih.Handle(), M.Times.Never);
            It does_not_call_intent_handler_B = () => _intentHandlerB.Verify(ih => ih.Handle(), M.Times.Never);
        }
    }
}
