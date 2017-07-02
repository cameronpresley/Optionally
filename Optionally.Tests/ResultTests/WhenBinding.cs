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
            Func<int, Result<string, Exception>> binder = num => Result<string, Exception>.Success(num.ToString());
            var input = new Exception();

            var observed = Result<int, Exception>.Failure(input).AndThen(binder);

            var expected = Result<string, Exception>.Failure(input);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndResultIsFailureThenBinderIsntCalled()
        {
            var wasBinderCalled = false;
            Func<int, Result<string, Exception>> binder = delegate (int number)
            {
                wasBinderCalled = true;
                return Result<string, Exception>.Success(number.ToString());
            };

            var observed = Result<int, Exception>.Failure(new Exception()).AndThen(binder);

            Assert.That(!wasBinderCalled);
        }

        [Test]
        public void AndResultIsSuccessThenBinderIsCalled()
        {
            var wasBinderCalled = false;
            Func<int, Result<string, Exception>> binder = delegate (int number)
            {
                wasBinderCalled = true;
                return Result<string, Exception>.Failure(new Exception());
            };

            var observed = Result<int, Exception>.Success(2).AndThen(binder);

            Assert.That(wasBinderCalled);
        }

        [Test]
        public void AndBinderIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Result<int, string>.Success(2).AndThen<Exception>(null));
        }
    }
}
