using System;
using NUnit.Framework;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    class WhenConstructingFailure
    {
        [Test]
        public void AndTheValueIsNullThenFailureIsReturned()
        {
            Assert.Throws<ArgumentNullException>(() => Result.Failure<string, int>(null));
        }

        [Test]
        public void AndTheValueIsNotNullThenFailureIsReturned()
        {
            var observed = Result.Failure<string, int>("Whoops");

            var expected = Result.Failure<string, int>("Whoops");
            Assert.AreEqual(expected, observed);
        }
    }
}
