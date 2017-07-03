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

            Result<string, Exception>.Failure(new Exception())
                .Do(s => Assert.Fail("Result is failure, should not be calling Success with input of " + s), failureAction);

            Assert.That(wasFailureActionCalled);
        }

        [Test]
        public void AndSuccessThenSuccessActionIsCalled()
        {
            var wasSuccessActionCalled = false;
            Action<int> successAction = i => wasSuccessActionCalled = true;

            Result<int, Exception>.Success(2)
                .Do(successAction, e => Assert.Fail("Result is success, should not be calling failure with input of " + e));

            Assert.That(wasSuccessActionCalled);
        }

        [Test]
        public void AndSuccessAndSuccessActionIsNullThenNothingHappens()
        {
            Assert.DoesNotThrow(() => Result<int, Exception>.Success(2).Do(null, _ => { }));
        }

        [Test]
        public void AndFailureAndFailureActionIsNullThenNothingHappens()
        {
            Assert.DoesNotThrow(() => Result<int, Exception>.Failure(new Exception()).Do(_ => { }, null));
        }

        [Test]
        public void AndSuccessAndBothActionsAreNullThenNothingHappens()
        {
            Assert.DoesNotThrow(() => Result<int, Exception>.Success(2).Do(null, null));
        }

        [Test]
        public void AndFailureAndBothActionsAreNullTheNothingHappens()
        {
            Assert.DoesNotThrow(() => Result<int, Exception>.Failure(new Exception()).Do(null, null));
        }
    }
}
