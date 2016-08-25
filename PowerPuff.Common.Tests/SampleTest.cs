using NUnit.Framework;

namespace PowerPuff.Common.Tests
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
