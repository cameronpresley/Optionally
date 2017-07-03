using NUnit.Framework;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    class WhenConstructingSuccess
    {
        [Test]
        public void AndTheValueIsNullThenSuccessIsReturned()
        {
            var observed = Result<string, int>.Success(null);

            var expected = Result<string, int>.Success(null);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheValueIsNotNullThenSuccessIsReturned()
        {
            var observed = Result<string, int>.Success("Success");

            var expected = Result<string, int>.Success("Success");
            Assert.AreEqual(expected, observed);
        }
    }
}
