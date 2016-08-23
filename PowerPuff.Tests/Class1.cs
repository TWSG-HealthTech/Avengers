using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PowerPuff.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void SampleTest()
        {
            Assert.That(1+1, Is.EqualTo(2));
        }
    }
}
