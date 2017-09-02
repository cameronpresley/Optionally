using NUnit.Framework;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    class WhenConstructingSuccess
    {
        [Test]
        public void AndTheValueIsNullThenSuccessIsReturned()
        {
            var observed = Result.Success<int, string>(null);

            var expected = Result.Success<int, string>(null);
            Assert.AreEqual(expected, observed);
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
