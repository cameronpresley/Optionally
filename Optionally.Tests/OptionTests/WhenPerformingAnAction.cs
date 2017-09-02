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

            Option.Some(4).Do(someAction, () => Assert.Fail("Option is Some, should not call None action"));

            Assert.That(wasSomeActionCalled);
        }

        [Test]
        public void AndNoneThenNoneActionIsCalled()
        {
            var wasNoneActionCalled = false;
            Action noneAction = () => wasNoneActionCalled = true;

            Option.No<int>().Do(x => Assert.Fail("Option is None, should not call Some action with param of " + x), noneAction);

            Assert.That(wasNoneActionCalled);
        }

        [Test]
        public void AndSomeAndSomeActionIsNullThenAnExcpetionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.Some(4).Do(null, () => { }));
        }

        [Test]
        public void AndNoneAndNoneActionIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.No<int>().Do(_ => { }, null));
        }

        [Test]
        public void AndNoneAndBothActionsAreNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.No<int>().Do(null, null));
        }

        [Test]
        public void AndSomeAndBothActionsAreNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.Some(2).Do(null, null));
        }
    }
}
