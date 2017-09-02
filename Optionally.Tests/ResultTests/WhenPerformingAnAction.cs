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

            void SuccessAction(string s) => Assert.Fail(
                "Result is failure, should not be calling Success with input of " + s);

            void FailureAction(Exception e) => wasFailureActionCalled = true;

            Result.Failure<Exception, string>(new Exception())
                .Do(FailureAction, SuccessAction);
            Assert.That(wasFailureActionCalled);
        }

        [Test]
        public void AndSuccessThenSuccessActionIsCalled()
        {
            var wasSuccessActionCalled = false;
            void SuccessAction(int i) => wasSuccessActionCalled = true;
            void FailureAction(Exception e) => Assert.Fail(
                "Result is success, should not be calling failure with input of " + e);

            Result.Success<Exception, int>(2)
                .Do(FailureAction, SuccessAction);
            Assert.That(wasSuccessActionCalled);
        }

        [Test]
        public void AndSuccessAndSuccessActionIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Result.Success<Exception, int>(2).Do(null, _ => { }));
        }

        [Test]
        public void AndSuccessAndFailureActionIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Result.Success<Exception, int>(5).Do(_ => { }, null));
        }

        [Test]
        public void AndFailureAndSuccessActionIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    Result
                    .Failure<Exception, int>(new Exception())
                    .Do(null, _ => { })
            );
        }

        [Test]
        public void AndFailureAndFailureActionIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Result.Failure<Exception, int>(new Exception()).Do(_ => { }, null));
        }
    }
}
