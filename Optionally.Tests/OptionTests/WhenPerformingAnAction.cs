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
            var someActionWasCalled = false;
            void SomeAction(int i) => someActionWasCalled = true;
            void NoneAction () => Assert.Fail("Option is Some, should not call None action.");

            Option.Some(4).Do(NoneAction, SomeAction);

            Assert.That(someActionWasCalled);
        }

        [Test]
        public void AndNoneThenNoneActionIsCalled()
        {
            var wasNoneActionCalled = false;
            void NoneAction () => wasNoneActionCalled = true;
            void SomeAction(int i) => Assert.Fail("Option is None, should not call Some action with param of " + i);

            Option.No<int>().Do(NoneAction, SomeAction);

            Assert.That(wasNoneActionCalled);
        }

        [Test]
        public void AndSomeAndSomeActionIsNullThenAnExcpetionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.Some(4).Do(() => { }, null));
        }

        [Test]
        public void AndSomeAndNoneActionIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.Some(4).Do(null, i => { }));
        }

        [Test]
        public void AndNoneAndSomeActionIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.No<int>().Do(() => { }, null));
        }

        [Test]
        public void AndNoneAndNoneActionIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.No<int>().Do(null, _ => { }));
        }
    }
}
