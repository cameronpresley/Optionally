using NUnit.Framework;

namespace Optionally.Tests.ConverterTests
{
    [TestFixture]
    public class WhenConvertingToDouble
    {
        [Test]
        public void AndInputIsADoubleThenSomeIsReturned()
        {
            var result = OptionConveter.ToDouble("12.34");

            var expected = Option.Some(12.34);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndInputIsAWholeNumberThenSomeIsReturned()
        {
            var result = OptionConveter.ToDouble("4");

            var expected = Option.Some(4.0);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndInputIsNotANumberThenNoneIsReturned()
        {
            var result = OptionConveter.ToDouble("kumquats");

            var expected = Option.No<double>();
            Assert.AreEqual(expected, result);
        }
    }
}
