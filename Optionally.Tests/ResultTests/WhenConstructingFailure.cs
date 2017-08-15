using NUnit.Framework;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    class WhenConstructingFailure
    {
        [Test]
        public void AndTheValueIsNullThenFailureIsReturned()
        {
            var observed = Result<string, int>.Failure(null);

            var expected = Result<string, int>.Failure(null);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheValueIsNotNullThenFailureIsReturned()
        {
            var observed = Result<string, int>.Failure("Whoops");

            var expected = Result<string, int>.Failure("Whoops");
            Assert.AreEqual(expected, observed);
        }
    }
}
