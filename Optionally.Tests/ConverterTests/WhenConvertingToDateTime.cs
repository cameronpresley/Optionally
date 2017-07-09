using System;
using NUnit.Framework;

namespace Optionally.Tests.ConverterTests
{
    [TestFixture]
    public class WhenConvertingToDateTime
    {
        [Test]
        public void AndInputIsADateTimeThenSomeIsReturned()
        {
            var result = OptionConveter.ToDateTime("12/12/2012");

            var expected = Option.Some(new DateTime(2012, 12, 12));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndInputIsNotADateTimeThenNoneIsReturned()
        {
            var result = OptionConveter.ToDateTime("kumquats");

            var expected = Option.No<DateTime>();
            Assert.AreEqual(expected, result);
        }
    }
}
