using NUnit.Framework;

namespace Optionally.Tests.ConverterTests
{
    [TestFixture]
    public class WhenConvertingToADecimal
    {
        [Test]
        public void AndInputIsANumberThenSomeIsReturned()
        {
            var result = OptionConveter.ToDecimal("12.34");

            var expected = Option.Some(12.34m);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndInputIsAWholeNumberThenSomeIsReturned()
        {
            var result = OptionConveter.ToDecimal("12");

            var expected = Option.Some(12m);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndInputIsNotANumberThenNoneIsReturned()
        {
            var result = OptionConveter.ToDecimal("something");

            var expected = Option.No<decimal>();
            Assert.AreEqual(expected, result);
        }

    }
}
