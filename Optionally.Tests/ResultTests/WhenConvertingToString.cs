using NUnit.Framework;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    public class WhenConvertingToString
    {
        [Test]
        public void AndTheResultIsFailureThenFailureStringIsReturned()
        {
            var result = Result<int, string>.Failure("failed result");

            Assert.AreEqual("Failure of 'failed result'", result.ToString());
        }

        [Test]
        public void AndTheResultIsSuccessThenSuccessStringIsReturned()
        {
            var result = Result<int, string>.Success(10);

            Assert.AreEqual("Success of '10'", result.ToString());
        }
    }
}
