using NUnit.Framework;

namespace Optionally.Tests.ConverterTests
{
    [TestFixture]
    public class WhenConvertingToABool
    {
        [Test]
        public void AndInputIsTrueThenSomeIsReturned()
        {
            var result = OptionConveter.ToBool("true");

            var expected = Option.Some(true);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndInputIs0ThenNoneIsReturned()
        {
            var result = OptionConveter.ToBool("0");

            var expected = Option.No<bool>();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndInputIsWordThenNoneIsReturned()
        {
            var result = OptionConveter.ToBool("word");

            var expected = Option.No<bool>();
            Assert.AreEqual(expected, result);
        }
    }
}
