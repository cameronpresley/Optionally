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
            Func<int, Result<Exception, string>> binder = num => Result<Exception, string>.Success(num.ToString());
            var input = new Exception();

            var observed = Result<Exception, int>.Failure(input).AndThen(binder);

            var expected = Result<Exception, string>.Failure(input);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndResultIsFailureThenBinderIsntCalled()
        {
            var wasBinderCalled = false;
            Func<int, Result<Exception, string>> binder = delegate (int number)
            {
                wasBinderCalled = true;
                return Result<Exception, string>.Success(number.ToString());
            };

            Result<Exception, int>.Failure(new Exception()).AndThen(binder);

            Assert.That(!wasBinderCalled);
        }

        [Test]
        public void AndResultIsSuccessThenBinderIsCalled()
        {
            var wasBinderCalled = false;
            Func<int, Result<Exception, string>> binder = delegate
            {
                wasBinderCalled = true;
                return Result<Exception, string>.Failure(new Exception());
            };

            Result<Exception, int>.Success(2).AndThen(binder);

            Assert.That(wasBinderCalled);
        }

        [Test]
        public void AndBinderIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Result<string, int>.Success(2).AndThen<Exception>(null));
        }
    }
}
