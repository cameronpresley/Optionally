using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Optionally.Tests.ConverterTests
{
    [TestFixture]
    public class WhenConvertingToAnInt
    {
        [Test]
        public void AndInputIsAnIntegerThenSomeIsReturned()
        {
            var result = Converter.ToInt("15");

            var expected = Option.Some(15);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndInputIsADoubleThenNoneIsReturned()
        {
            var result = Converter.ToInt("15.2");
            
            var expected = Option.No<int>();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndInputIsNotANumberThenNoneIsReturned()
        {
            var result = Converter.ToInt("kumquats");

            var expected = Option.No<int>();
            Assert.AreEqual(expected, result);
        }
    }
}
