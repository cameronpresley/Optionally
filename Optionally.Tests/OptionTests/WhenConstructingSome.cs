using System;
using NUnit.Framework;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    public class WhenConstructingSome
    {
        [Test]
        public void AndTheValueIsNullThenNoneIsConstructed()
        {
            Assert.Throws<ArgumentNullException>(() => Option.Some<object>(null));
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
