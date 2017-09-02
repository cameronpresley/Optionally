using NUnit.Framework;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    class WhenConstructingFailure
    {
        [Test]
        public void AndTheValueIsNullThenFailureIsReturned()
        {
            var observed = Result.Failure<string, int>(null);

            var expected = Result.Failure<string, int>(null);
            Assert.AreEqual(expected, observed);
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
