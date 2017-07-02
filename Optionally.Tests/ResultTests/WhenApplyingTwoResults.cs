using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    class WhenApplyingTwoResults
    {
        [Test]
        public void AndTheFunctionIsNullThenAnExceptionIsThrown()
        {
            var first = CreateSuccess(2);
            var second = CreateSuccess(4);
            Assert.Throws<ArgumentNullException>(() => Result<int, Exception>.Apply(null, first, second));
        }

        [Test]
        public void AndTheFirstResultIsNullThenAnExceptionIsThrown()
        {
            var second = CreateSuccess(2);
            Func<int, int, int> add = (a, b) => a + b;

            Assert.Throws<ArgumentNullException>(() => Result<int, Exception>.Apply(add, null, second));
        }

        [Test]
        public void AndTheSecondResultIsNullThenAnExceptionIsThrown()
        {
            var first = CreateSuccess(2);
            Func<int, int, int> add = (a, b) => a + b;

            Assert.Throws<ArgumentNullException>(() => Result<int, Exception>.Apply(add, first, null));
        }

        [Test]
        public void AndTheFirstResultIsAFailureThenFailureIsReturned()
        {
            var exception = new Exception();
            var first = CreateFailure(exception);
            var second = CreateSuccess(2);
            Func<int, int, int> add = (a, b) => a + b;

            var observed = Result<int, Exception>.Apply(add, first, second);

            var expectedErrors = new List<Exception> { exception };
            observed
                .Do(funcResult => Assert.Fail("Result should not be Success with value of " + funcResult), 
                    errors => CollectionAssert.AreEqual(expectedErrors, errors));
        }

        [Test]
        public void AndTheSecondResultIsFailureThenAFailureIsReturned()
        {
            var first = CreateSuccess(2);
            var exception = new Exception();
            var second = CreateFailure(exception);
            Func<int, int, int> add = (a, b) => a + b;

            var observed = Result<int, Exception>.Apply(add, first, second);

            var expectedErrors = new List<Exception> { exception };
            observed
                .Do(funcResult => Assert.Fail("Result should not be Success with value of " + funcResult),
                    errors => CollectionAssert.AreEqual(expectedErrors, errors));
        }

        [Test]
        public void AndBothResultsAreFailuresThenTheFailuresAreConcatenated()
        {
            var firstException = new Exception("Wasn't valid.");
            var first = CreateFailure(firstException);
            var secondException = new Exception("Neither was this one.");
            var second = CreateFailure(secondException);
            Func<int, int, int> add = (a, b) => a + b;

            var observed = Result<int, Exception>.Apply(add, first, second);

            var expectedErrors = new List<Exception> { firstException, secondException};
            observed
                .Do(funcResult => Assert.Fail("Result should not be Success with value of " + funcResult),
                    errors => CollectionAssert.AreEqual(expectedErrors, errors));
        }

        [Test]
        public void AndBothResultsAreSuccessThenTheFunctionIsCalledInSuccess()
        {
            var first = CreateSuccess(2);
            var second = CreateSuccess(4);
            Func<int, int, int> add = (a, b) => a + b;

            var observed = Result<int, Exception>.Apply(add, first, second);

            var expected = Result<int, List<Exception>>.Success(add(2, 4));
            Assert.AreEqual(expected, observed);
        }

        private Result<int, Exception>CreateFailure(Exception e)
        {
            return Result<int, Exception>.Failure(e);
        }

        private Result<int, Exception>CreateSuccess(int i)
        {
            return Result<int, Exception>.Success(i);
        }
    }
}
