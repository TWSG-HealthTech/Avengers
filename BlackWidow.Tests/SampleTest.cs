using NUnit.Framework;

namespace BlackWidow.Tests
{
    [TestFixture]
    public class SampleTest
    {
        [Test]
        public void Test()
        {
            Assert.That(1 + 1, Is.EqualTo(2));
        }
    }
}
