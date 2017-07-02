using NUnit.Framework;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    class WhenConstructingFailure
    {
        [Test]
        public void AndTheValueIsNullThenFailureIsReturned()
        {
            var observed = Result<int, string>.Failure(null);

            var expected = Result<int, string>.Failure(null);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheValueIsNotNullThenFailureIsReturned()
        {
            var observed = Result<int, string>.Failure("Whoops");

            var expected = Result<int, string>.Failure("Whoops");
            Assert.AreEqual(expected, observed);
        }
    }
}
