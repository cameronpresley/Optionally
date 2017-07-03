using NUnit.Framework;

namespace Optionally.Tests.NullableExtensionTests
{
    [TestFixture]
    class WhenConvertingANullableToAnOption
    {
        [Test]
        public void AndTheNullableIsNullThenNoneIsReturned()
        {
            var input = new int?();

            var observed = input.ToOption();

            var expected = Option<int>.None();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheNullableHasAValueThenSomeIsReturned()
        {
            var input = new int?(2);

            var observed = input.ToOption();

            var expected = Option<int>.Some(2);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheInputIsNullThenNoneIsReturned()
        {
            var input = (int?)null;

            var observed = input.ToOption();

            var expected = Option<int>.None();
            Assert.AreEqual(expected, observed);
        }
    }
}
