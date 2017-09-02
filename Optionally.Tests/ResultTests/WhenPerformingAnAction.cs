using NUnit.Framework;
using System;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    class WhenPerformingAnAction
    {
        [Test]
        public void AndFailureThenFailureActionIsCalled()
        {
            var wasFailureActionCalled = false;
            Action<Exception> failureAction = e => wasFailureActionCalled = true;

            Result.Failure<Exception, string>(new Exception())
                .Do(s => Assert.Fail("Result is failure, should not be calling Success with input of " + s), failureAction);

            Assert.That(wasFailureActionCalled);
        }

        [Test]
        public void AndSuccessThenSuccessActionIsCalled()
        {
            var wasSuccessActionCalled = false;
            Action<int> successAction = i => wasSuccessActionCalled = true;

            Result.Success<Exception, int>(2)
                .Do(successAction, e => Assert.Fail("Result is success, should not be calling failure with input of " + e));

            Assert.That(wasSuccessActionCalled);
        }

        [Test]
        public void AndSuccessAndSuccessActionIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Result.Success<Exception, int>(2).Do(null, _ => { }));
        }

        [Test]
        public void AndFailureAndFailureActionIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Result.Failure<Exception, int>(new Exception()).Do(_ => { }, null));
        }
    }
}
