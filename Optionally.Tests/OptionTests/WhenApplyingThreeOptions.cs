using NUnit.Framework;
using System;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    class WhenApplyingThreeOptions
    {
        [Test]
        public void AndTheFirstOptionIsNoneThenNoneIsReturned()
        {
            var first = Option<int>.None;
            var second = Option<int>.Some(2);
            var third = Option<int>.Some(4);
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Option<int>.Apply(add, first, second, third);

            var expected = Option<int>.None;
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheSecondOptionIsNoneThenNoneIsReturned()
        {
            var first = Option<int>.Some(2);
            var second = Option<int>.None;
            var third = Option<int>.Some(4);
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Option<int>.Apply(add, first, second, third);

            var expected = Option<int>.None;
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheThirdOptionIsNoneThenNoneIsReturned()
        {
            var first = Option<int>.Some(2);
            var second = Option<int>.Some(4);
            var third = Option<int>.None;
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Option<int>.Apply(add, first, second, third);

            var expected = Option<int>.None;
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheOptionsAreSomeThenTheFunctionIsInvoked()
        {
            var first = Option<int>.Some(2);
            var second = Option<int>.Some(4);
            var third = Option<int>.Some(8);
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Option<int>.Apply(add, first, second, third);

            var expected = Option<int>.Some(add(2, 4, 8));
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheFunctionIsNullThenNoneIsReturned()
        {
            var first = Option<int>.Some(2);
            var second = Option<int>.Some(4);
            var third = Option<int>.Some(8);

            var observed = Option<int>.Apply(null, first, second, third);

            var expected = Option<int>.None;
            Assert.AreEqual(expected, observed);
        }
    }
}
