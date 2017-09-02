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
            Func<int, int, int> func = null;

            Assert.Throws<ArgumentNullException>(() => Result.Apply(func, first, second));
        }

        [Test]
        public void AndTheFirstResultIsAFailureThenFailureIsReturned()
        {
            var exception = new Exception();
            var first = CreateFailure(exception);
            var second = CreateSuccess(2);
            int Add(int a, int b) => a + b;

            var observed = Result.Apply(Add, first, second);

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
            int Add(int a, int b) => a + b;

            var observed = Result.Apply(Add, first, second);

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
            int Add(int a, int b) => a + b;

            var observed = Result.Apply(Add, first, second);

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
            int Add(int a, int b) => a + b;

            var observed = Result.Apply(Add, first, second);

            var expected = Result.Success<IEnumerable<Exception>, int>(Add(2, 4));
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheFirstResultIsNullThenAnExceptionIsThrown()
        {
            int Add(int a, int b) => a + b;
            IResult<Exception, int> first = null;

            Assert.Throws<ArgumentNullException>(() => Result.Apply(Add, first, CreateSuccess(2)));
        }

        [Test]
        public void AndTheSecondResultIsNullThenAnExceptionIsThrown()
        {
            int Add(int a, int b) => a + b;
            IResult<Exception, int> second = null;

            Assert.Throws<ArgumentNullException>(() => Result.Apply(Add, CreateSuccess(2), second));
        }

        private IResult<Exception, int>CreateFailure(Exception e)
        {
            return Result.Failure<Exception, int>(e);
        }

        private IResult<Exception, int>CreateSuccess(int i)
        {
            return Result.Success<Exception, int>(i);
        }
    }
}
