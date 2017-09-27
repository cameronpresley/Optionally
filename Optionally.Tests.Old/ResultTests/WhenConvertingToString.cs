using NUnit.Framework;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    public class WhenConvertingToString
    {
        [Test]
        public void AndTheResultIsFailureThenFailureStringIsReturned()
        {
            var result = Result.Failure<string, int>("failed result");

            Assert.AreEqual("Failure of 'failed result'", result.ToString());
        }

        [Test]
        public void AndTheResultIsSuccessThenSuccessStringIsReturned()
        {
            var result = Result.Success<string, int>(10);

            Assert.AreEqual("Success of '10'", result.ToString());
        }
    }
}
