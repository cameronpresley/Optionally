using NUnit.Framework;

namespace Optionally.Tests.ConverterTests
{
    [TestFixture]
    public class WhenConvertingToAnInt
    {
        [Test]
        public void AndInputIsAnIntegerThenSomeIsReturned()
        {
            var result = OptionConveter.ToInt("15");

            var expected = Option.Some(15);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndInputIsADoubleThenNoneIsReturned()
        {
            var result = OptionConveter.ToInt("15.2");
            
            var expected = Option.No<int>();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndInputIsNotANumberThenNoneIsReturned()
        {
            var result = OptionConveter.ToInt("kumquats");

            var expected = Option.No<int>();
            Assert.AreEqual(expected, result);
        }
    }
}
