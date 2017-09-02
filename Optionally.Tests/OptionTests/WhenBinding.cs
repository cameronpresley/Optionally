using NUnit.Framework;
using System;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    class WhenBinding
    {
        [Test]
        public void AndOptionIsNoneThenNoneIsReturned()
        {
            IOption<double> SquareRoot(int i)
            {
                if (i < 0) return Option.No<double>();
                return Option.Some(Math.Sqrt(i));
            }

            var result = Option.No<int>().AndThen(SquareRoot);

            var expected = Option.No<double>();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndOptionIsSomeAndTheBinderIsCalled()
        {
            var binderWasCalled = false;
            IOption<string> Binder(int arg)
            {
                binderWasCalled = true;
                return Option.No<string>();
            }

            Option.Some(4).AndThen(Binder);

            Assert.That(binderWasCalled);
        }

        [Test]
        public void AndOptionIsSomeAndBinderIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.Some(4).AndThen<string>(null));
        }

        [Test]
        public void AndOptionIsNoneAndBinderIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.No<int>().AndThen<string>(null));
        }
    }
}
