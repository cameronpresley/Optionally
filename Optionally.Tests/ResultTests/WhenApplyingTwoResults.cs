﻿using NUnit.Framework;
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
            Func<int, int, int> add = (a, b) => a + b;

            var observed = Result.Apply(add, first, second);

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

            var observed = Result.Apply(add, first, second);

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

            var observed = Result.Apply(add, first, second);

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

            var observed = Result.Apply(add, first, second);

            var expected = Result<IEnumerable<Exception>, int>.Success(add(2, 4));
            Assert.AreEqual(expected, observed);
        }

        private Result<Exception, int>CreateFailure(Exception e)
        {
            return Result<Exception, int>.Failure(e);
        }

        private Result<Exception, int>CreateSuccess(int i)
        {
            return Result<Exception, int>.Success(i);
        }
    }
}
