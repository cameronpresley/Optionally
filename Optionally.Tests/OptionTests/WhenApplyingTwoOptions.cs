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
            int Add(int a, int b) => a + b;

            var observed = Option.Apply(Add, first, second);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheSecondOptionIsNoneThenNoneIsReturned()
        {
            var first = Option.Some(2);
            var second = Option.No<int>();
            int Add(int a, int b) => a + b;

            var observed = Option.Apply(Add, first, second);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndBothOptionsAreSomeThenTheFunctionIsInvoked()
        {
            var first = Option.Some(2);
            var second = Option.Some(4);
            int Add(int a, int b) => a + b;

            var observed = Option.Apply(Add, first, second);

            var expected = Option.Some(Add(2, 4));
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
