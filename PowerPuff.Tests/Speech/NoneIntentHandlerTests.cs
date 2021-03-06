﻿using Machine.Specifications;
using PowerPuff.Common.Speech;
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

        private Because of = async () => await _subject.DefaultHandler(null, null);

        It says_that_it_does_not_know = () => _speechSynthesiserMock.Verify(ss => ss.Speak("I'm sorry, I don't know how to do that."));
    }
}
