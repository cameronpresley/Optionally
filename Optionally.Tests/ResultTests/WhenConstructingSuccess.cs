using NUnit.Framework;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    class WhenConstructingSuccess
    {
        [Test]
        public void AndTheValueIsNullThenSuccessIsReturned()
        {
            var observed = Result<int, string>.Success(null);

            var expected = Result<int, string>.Success(null);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheValueIsNotNullThenSuccessIsReturned()
        {
            var observed = Result<int, string>.Success("Success");

            var expected = Result<int, string>.Success("Success");
            Assert.AreEqual(expected, observed);
        }
    }
}
