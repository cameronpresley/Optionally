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
            var first = Option.No<int>();
            var second = Option.Some(2);
            var third = Option.Some(4);
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Option.Apply(add, first, second, third);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheSecondOptionIsNoneThenNoneIsReturned()
        {
            var first = Option.Some(2);
            var second = Option.No<int>();
            var third = Option.Some(4);
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Option.Apply(add, first, second, third);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheThirdOptionIsNoneThenNoneIsReturned()
        {
            var first = Option.Some(2);
            var second = Option.Some(4);
            var third = Option.No<int>();
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Option.Apply(add, first, second, third);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheOptionsAreSomeThenTheFunctionIsInvoked()
        {
            var first = Option.Some(2);
            var second = Option.Some(4);
            var third = Option.Some(8);
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Option.Apply(add, first, second, third);

            var expected = Option.Some(add(2, 4, 8));
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheFunctionIsNullThenNoneIsReturned()
        {
            var first = Option.Some(2);
            var second = Option.Some(4);
            var third = Option.Some(8);

            Assert.Throws<ArgumentNullException>(() => Option.Apply((Func<int, int, int,int>)null, first, second, third));
        }
    }
}
