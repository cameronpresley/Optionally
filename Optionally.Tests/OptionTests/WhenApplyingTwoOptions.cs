using NUnit.Framework;
using System;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    class WhenApplyingTwoOptions
    {
        [Test]
        public void AndTheFirstOptionIsNoneThenNoneIsReturned()
        {
            var first = Option<int>.None;
            var second = Option<int>.Some(2);
            Func<int, int, int> add = (a, b) => a + b;

            var observed = Option<int>.Apply(add, first, second);

            var expected = Option<int>.None;
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheSecondOptionIsNoneThenNoneIsReturned()
        {
            var first = Option<int>.Some(2);
            var second = Option<int>.None;
            Func<int, int, int> add = (a, b) => a + b;

            var observed = Option<int>.Apply(add, first, second);

            var expected = Option<int>.None;
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndBothOptionsAreSomeThenTheFunctionIsInvoked()
        {
            var first = Option<int>.Some(2);
            var second = Option<int>.Some(4);
            Func<int, int, int> add = (a, b) => a + b;

            var observed = Option<int>.Apply(add, first, second);

            var expected = Option<int>.Some(add(2, 4));
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheFunctionIsNullThenNoneIsReturned()
        {
            var first = Option<int>.Some(2);
            var second = Option<int>.Some(4);

            var observed = Option<int>.Apply(null, first, second);

            var expected = Option<int>.None;
            Assert.AreEqual(expected, observed);
        }
    }
}
