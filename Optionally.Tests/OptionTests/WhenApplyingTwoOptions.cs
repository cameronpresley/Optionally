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
            var first = Option.No<int>();
            var second = Option.Some(2);
            Func<int, int, int> add = (a, b) => a + b;

            var observed = Option.Apply(add, first, second);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheSecondOptionIsNoneThenNoneIsReturned()
        {
            var first = Option.Some(2);
            var second = Option.No<int>();
            Func<int, int, int> add = (a, b) => a + b;

            var observed = Option.Apply(add, first, second);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndBothOptionsAreSomeThenTheFunctionIsInvoked()
        {
            var first = Option.Some(2);
            var second = Option.Some(4);
            Func<int, int, int> add = (a, b) => a + b;

            var observed = Option.Apply(add, first, second);

            var expected = Option.Some(add(2, 4));
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheFunctionIsNullThenNoneIsReturned()
        {
            var first = Option.Some(2);
            var second = Option.Some(4);

            Assert.Throws<ArgumentNullException>(() => Option.Apply((Func<int, int, int>)null, first, second));
        }
    }
}
