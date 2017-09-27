using System;
using NUnit.Framework;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    public class WhenWrapping
    {
        [Test]
        public void AndTheFuncReturnsAValueThenSuccessIsReturned()
        {
            var funcWasCalled = false;
            int GetValue()
            {
                funcWasCalled = true;
                return 2;
            }

            var observed = Result.Wrap(GetValue);

            var expected = Result.Success<Exception, int>(2);
            Assert.AreEqual(expected, observed);
            Assert.That(funcWasCalled);
        }

        [Test]
        public void AndTheFuncThrowsAnExceptionThenFailureIsReturned()
        {
            var exception = new Exception("message");
            int GetValue() => throw exception;

            var observed = Result.Wrap(GetValue);

            var expected = Result.Failure<Exception, int>(exception);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheFuncIsNullThenFailureIsReturned()
        {
            Func<int> getValue = null;

            var observed = Result.Wrap(getValue);

            observed.Do(ex => Assert.Pass(), _ => Assert.Fail());
        }
    }
}
