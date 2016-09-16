using Machine.Specifications;
using PowerPuff.Common.Helpers;
using PowerPuff.Features.Timer.Sound;
using System.IO;
using M = Moq;

namespace PowerPuff.Features.Timer.Tests.Sound
{
    [Subject(typeof(Alerter))]
    class AlerterTests
    {
        private static M.Mock<ISoundPlayer> _soundPlayerMock;
        private static Alerter _subject;

        Establish context = () =>
        {
            _soundPlayerMock = new M.Mock<ISoundPlayer>();
            _subject = new Alerter(_soundPlayerMock.Object);
        };

        Because of = () => _subject.Alert();

        It plays_a_sound = () => _soundPlayerMock.Verify(s => s.Play(M.It.IsAny<Stream>()));
    }
}
