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
            var result = Option<int>.None().AndThen(i => Option<string>.Some(i.ToString()));

            var expected = Option<string>.None();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndOptionIsSomeAndTheBinderIsCalled()
        {
            var binderWasCalled = false;
            Func<int, Option<string>> binder = delegate (int i)
            {
                binderWasCalled = true;
                return Option<string>.None();
            };

            Option<int>.Some(4).AndThen(binder);

            Assert.That(binderWasCalled);
        }

        [Test]
        public void AndBinderIsNullThenNoneIsReturned()
        {
            var result = Option<int>.Some(4).AndThen<string>(null);

            var expected = Option<string>.None();
            Assert.AreEqual(expected, result);
        }
    }
}
