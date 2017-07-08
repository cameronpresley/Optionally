using NUnit.Framework;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    public class WhenConstructingSome
    {
        [Test]
        public void AndTheValueIsNullThenNoneIsConstructed()
        {
            var observed = Option.Some<object>(null);

            var expected = Option.No<object>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheValueIsNotNullThenSomeIsConstructed()
        {
            var input = 4;
            var observed = Option.Some(input);

            var expected = Option.Some(4);
            Assert.AreEqual(expected, observed);
        }
    }
}
