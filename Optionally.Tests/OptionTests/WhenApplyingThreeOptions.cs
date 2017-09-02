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
            int Add(int a, int b, int c) => a + b + c;

            var observed = Option.Apply(Add, first, second, third);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheSecondOptionIsNoneThenNoneIsReturned()
        {
            var first = Option.Some(2);
            var second = Option.No<int>();
            var third = Option.Some(4);
            int Add(int a, int b, int c) => a + b + c;

            var observed = Option.Apply(Add, first, second, third);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheThirdOptionIsNoneThenNoneIsReturned()
        {
            var first = Option.Some(2);
            var second = Option.Some(4);
            var third = Option.No<int>();
            int Add(int a, int b, int c) => a + b + c;

            var observed = Option.Apply(Add, first, second, third);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheOptionsAreSomeThenTheFunctionIsInvoked()
        {
            var first = Option.Some(2);
            var second = Option.Some(4);
            var third = Option.Some(8);
            int Add(int a, int b, int c) => a + b + c;

            var observed = Option.Apply(Add, first, second, third);

            var expected = Option.Some(Add(2, 4, 8));
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

        [Test]
        public void AndTheFirstParamIsNullThenAnExceptionIsThrown()
        {
            var first = (IOption<int>) null;
            var second = Option.Some(4);
            var third = Option.Some(8);
            int Add(int a, int b, int c) => a + b + c;

            Assert.Throws<ArgumentNullException>(() => Option.Apply(Add, first, second, third));
        }

        [Test]
        public void AndTheSecondParamIsNullThenAnExceptionIsThrown()
        {
            var first = Option.Some(4);
            var second = (IOption<int>)null;
            var third = Option.Some(8);
            int Add(int a, int b, int c) => a + b + c;

            Assert.Throws<ArgumentNullException>(() => Option.Apply(Add, first, second, third));
        }

        [Test]
        public void AndTheThirdParamIsNullThenAnExceptionIsThrown()
        {
            var first = Option.Some(8);
            var second = Option.Some(4);
            var third = (IOption<int>)null;
            int Add(int a, int b, int c) => a + b + c;

            Assert.Throws<ArgumentNullException>(() => Option.Apply(Add, first, second, third));
        }
    }
}
