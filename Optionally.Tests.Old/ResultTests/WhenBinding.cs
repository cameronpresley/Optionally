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
            IResult<Exception, string> Binder(int num) => Result.Success<Exception, string>(num.ToString());
            var input = new Exception();

            var observed = Result.Failure<Exception, int>(input)
                                    .AndThen(Binder);

            var expected = Result.Failure<Exception, string>(input);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndResultIsFailureThenBinderIsntCalled()
        {
            var binderWasCalled = false;

            IResult<Exception, string> Binder(int number)
            {
                binderWasCalled = true;
                return Result.Success<Exception, string>(number.ToString());
            }

            Result.Failure<Exception, int>(new Exception())
                .AndThen(Binder);

            Assert.That(!binderWasCalled);
        }

        [Test]
        public void AndResultIsSuccessThenBinderIsCalled()
        {
            var binderWasCalled = false;

            IResult<Exception, string> Binder(int arg)
            {
                binderWasCalled = true;
                return Result.Failure<Exception, string>(new Exception());
            }

            Result.Success<Exception, int>(2)
                    .AndThen(Binder);

            Assert.That(binderWasCalled);
        }

        [Test]
        public void AndSuccessAndBinderIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Result.Success<string, int>(2).AndThen<Exception>(null));
        }

        [Test]
        public void AndFailureAndBinderIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Result.Failure<string, int>("failure").AndThen<Exception>(null));
        }
    }
}
