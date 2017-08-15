using NUnit.Framework;
using Optionally.Extensions;

namespace Optionally.Tests.Extensions.NullableExtensionTests
{
    [TestFixture]
    class WhenConvertingANullableToAnOption
    {
        [Test]
        public void AndTheNullableIsNullThenNoneIsReturned()
        {
            var input = new int?();

            var observed = input.ToOption();

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheNullableHasAValueThenSomeIsReturned()
        {
            var input = new int?(2);

            var observed = input.ToOption();

            var expected = Option.Some(2);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheInputIsNullThenNoneIsReturned()
        {
            var input = (int?)null;

            var observed = input.ToOption();

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }
    }
}
