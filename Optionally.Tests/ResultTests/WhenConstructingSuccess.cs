using System;
using NUnit.Framework;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    class WhenConstructingSuccess
    {
        [Test]
        public void AndTheValueIsNullThenSuccessIsReturned()
        {
            Assert.Throws<ArgumentNullException>(() => Result.Success<int, string>(null));
        }

        [Test]
        public void AndTheValueIsNotNullThenSuccessIsReturned()
        {
            var observed = Result.Success<int, string>("Success");

            var expected = Result.Success<int, string>("Success");
            Assert.AreEqual(expected, observed);
        }
    }
}
