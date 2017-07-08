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
            var result = Option.No<int>().AndThen(i => Option.Some(i.ToString()));

            var expected = Option.No<string>();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndOptionIsSomeAndTheBinderIsCalled()
        {
            var binderWasCalled = false;
            Func<int, Option<string>> binder = delegate (int i)
            {
                binderWasCalled = true;
                return Option.No<string>();
            };

            Option.Some(4).AndThen(binder);

            Assert.That(binderWasCalled);
        }

        [Test]
        public void AndBinderIsNullThenNoneIsReturned()
        {
            var result = Option.Some(4).AndThen<string>(null);

            var expected = Option.No<string>();
            Assert.AreEqual(expected, result);
        }
    }
}
