﻿using NUnit.Framework;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    public class WhenConstructingSome
    {
        [Test]
        public void AndTheValueIsNullThenNoneIsConstructed()
        {
            object input = null;
            var observed = Option<object>.Some(input);

            var expected = Option<object>.None();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheValueIsNotNullThenSomeIsConstructed()
        {
            var input = 4;
            var observed = Option<int>.Some(input);

            var expected = Option<int>.Some(4);
            Assert.AreEqual(expected, observed);
        }
    }
}
