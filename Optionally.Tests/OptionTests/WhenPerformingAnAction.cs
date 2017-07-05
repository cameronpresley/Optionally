using NUnit.Framework;
using System;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    class WhenPerformingAnAction
    {
        [Test]
        public void AndSomeThenTheSomeActionIsCalled()
        {
            var wasSomeActionCalled = false;
            Action<int> someAction = i => wasSomeActionCalled = true;

            Option<int>.Some(4).Do(someAction, () => Assert.Fail("Option is Some, should not call None action"));

            Assert.That(wasSomeActionCalled);
        }

        [Test]
        public void AndNoneThenNoneActionIsCalled()
        {
            var wasNoneActionCalled = false;
            Action noneAction = () => wasNoneActionCalled = true;

            Option<int>.None.Do(x => Assert.Fail("Option is None, should not call Some action with param of " + x), noneAction);

            Assert.That(wasNoneActionCalled);
        }

        [Test]
        public void AndSomeAndSomeActionIsNullThenNothingHappens()
        {
            Assert.DoesNotThrow(() => Option<int>.Some(4).Do(null, () => { }));
        }

        [Test]
        public void AndNoneAndNoneActionIsNullThenNothingHappens()
        {
            Assert.DoesNotThrow(() => Option<int>.None.Do(_ => { }, null));
        }

        [Test]
        public void AndNoneAndBothActionsAreNullThenNothingHappens()
        {
            Assert.DoesNotThrow(() => Option<int>.None.Do(null, null));
        }

        [Test]
        public void AndSomeAndBothActionsAreNullThenNothingHappens()
        {
            Assert.DoesNotThrow(() => Option<int>.Some(2).Do(null, null));
        }
    }
}
