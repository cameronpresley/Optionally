using NUnit.Framework;
using System;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    class WhenBinding
    {
        [Test]
        public void AndResultIsFailureThenFailureIsReturned()
        {
            Func<int, IResult<Exception, string>> binder = num => Result.Success<Exception, string>(num.ToString());
            var input = new Exception();

            var observed = Result.Failure<Exception, int>(input).AndThen(binder);

            var expected = Result.Failure<Exception, string>(input);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndResultIsFailureThenBinderIsntCalled()
        {
            var wasBinderCalled = false;
            Func<int, IResult<Exception, string>> binder = delegate (int number)
            {
                wasBinderCalled = true;
                return Result.Success<Exception, string>(number.ToString());
            };

            Result.Failure<Exception, int>(new Exception()).AndThen(binder);

            Assert.That(!wasBinderCalled);
        }

        [Test]
        public void AndResultIsSuccessThenBinderIsCalled()
        {
            var wasBinderCalled = false;
            Func<int, IResult<Exception, string>> binder = delegate
            {
                wasBinderCalled = true;
                return Result.Failure<Exception, string>(new Exception());
            };

            Result.Success<Exception, int>(2).AndThen(binder);

            Assert.That(wasBinderCalled);
        }

        [Test]
        public void AndBinderIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Result.Success<string, int>(2).AndThen<Exception>(null));
        }
    }
}
