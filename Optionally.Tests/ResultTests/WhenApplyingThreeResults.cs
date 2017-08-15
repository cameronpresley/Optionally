﻿using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    class WhenApplyingThreeResults
    {
        [Test]
        public void AndTheFunctionIsNullThenAnExceptionIsThrown()
        {
            var first = CreateSuccess(2);
            var second = CreateSuccess(4);
            var third = CreateSuccess(8);
            Func<int, int, int, int> func = null;

            Assert.Throws<ArgumentNullException>(() => Result.Apply(func, first, second, third));
        }

        [Test]
        public void AndThreeResultsAreSuccessThenSuccessIsReturned()
        {
            var first = CreateSuccess(2);
            var second = CreateSuccess(4);
            var third = CreateSuccess(8);
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Result.Apply(add, first, second, third);

            var expected = Result<IEnumerable<Exception>, int>.Success(add(2, 4, 8));
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndThreeResultsAreFailuresThenFailureListIsReturned()
        {
            var firstException = new Exception("first");
            var first = CreateFailure(firstException);
            var secondException = new Exception("second");
            var second = CreateFailure(secondException);
            var thirdException = new Exception("third");
            var third = CreateFailure(thirdException);

            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Result.Apply(add, first, second, third);

            var expectedErrors = new List<Exception> { firstException, secondException, thirdException };
            observed.Do(
                i => Assert.Fail("Result should be Failure, not Success with value of " + i),
                errors => CollectionAssert.AreEqual(expectedErrors, errors));
        }

        [Test]
        public void AndTheFirstResultIsFailureThenFailureIsReturned()
        {
            var firstException = new Exception("first");
            var first = CreateFailure(firstException);
            var second = CreateSuccess(2);
            var third = CreateSuccess(4);
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Result.Apply(add, first, second, third);

            var expectedErrors = new List<Exception> { firstException };
            observed.Do(
                i => Assert.Fail("Result should be Failure, not Success with value of " + i),
                errors => CollectionAssert.AreEqual(expectedErrors, errors)
                );
        }

        [Test]
        public void AndTheSecondResultIsFailureThenFailureIsReturned()
        {
            var first = CreateSuccess(2);
            var secondException = new Exception("second");
            var second = CreateFailure(secondException);
            var third = CreateSuccess(4);
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Result.Apply(add, first, second, third);

            var expectedErrors = new List<Exception> { secondException };
            observed.Do(
                i => Assert.Fail("Result should be Failure, not Success with value of " + i),
                errors => CollectionAssert.AreEqual(expectedErrors, errors)
                );
        }

        [Test]
        public void AndTheThirdResultIsFailureThenFailureIsReturned()
        {
            var first = CreateSuccess(2);
            var second = CreateSuccess(4);
            var thirdException = new Exception("third");
            var third = CreateFailure(thirdException);
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Result.Apply(add, first, second, third);

            var expectedErrors = new List<Exception> { thirdException };
            observed.Do(
                i => Assert.Fail("Result should be Failure, not Success with value of " + i),
                errors => CollectionAssert.AreEqual(expectedErrors, errors)
                );
        }

        [Test]
        public void AndTheFirstTwoResultsAreFailuresThenFailureIsReturned()
        {
            var firstException = new Exception("first");
            var first = CreateFailure(firstException);
            var secondException = new Exception("second");
            var second = CreateFailure(secondException);
            var third = CreateSuccess(4);
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Result.Apply(add, first, second, third);

            var expectedErrors = new List<Exception> { firstException, secondException };
            observed.Do(
                i => Assert.Fail("Result should be Failure, not Success with value of " + i),
                errors => CollectionAssert.AreEqual(expectedErrors, errors)
                );
        }

        [Test]
        public void AndTheLastTwoResultsAreFailuresThenFailureIsReturned()
        {
            var first = CreateSuccess(2);
            var secondException = new Exception("second");
            var second = CreateFailure(secondException);
            var thirdException = new Exception("third");
            var third = CreateFailure(thirdException);
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Result.Apply(add, first, second, third);

            var expectedErrors = new List<Exception> { secondException, thirdException };
            observed.Do(
                i => Assert.Fail("Result should be Failure, not Success with value of " + i),
                errors => CollectionAssert.AreEqual(expectedErrors, errors)
                );
        }

        [Test]
        public void AndTheFirstAndLastResultsAreFailuresThenFailureIsReturned()
        {
            var firstException = new Exception("first");
            var first = CreateFailure(firstException);
            var second = CreateSuccess(2);
            var thirdException = new Exception("third");
            var third = CreateFailure(thirdException);
            Func<int, int, int, int> add = (a, b, c) => a + b + c;

            var observed = Result.Apply(add, first, second, third);

            var expectedErrors = new List<Exception> { firstException, thirdException };
            observed.Do(
                i => Assert.Fail("Result should be Failure, not Success with value of " + i),
                errors => CollectionAssert.AreEqual(expectedErrors, errors)
                );
        }


        private Result<Exception, int> CreateSuccess(int i)
        {
            return Result<Exception, int>.Success(i);
        }

        private Result<Exception, int> CreateFailure(Exception e)
        {
            return Result<Exception, int>.Failure(e);
        }
    }
}
