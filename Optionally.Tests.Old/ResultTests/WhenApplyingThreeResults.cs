using NUnit.Framework;
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
            int Add (int a, int b, int c) => a + b + c;

            var observed = Result.Apply(Add, first, second, third);

            var expected = Result.Success<IEnumerable<Exception>, int>(Add(2, 4, 8));
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

            int Add(int a, int b, int c) => a + b + c;

            var observed = Result.Apply(Add, first, second, third);

            var expectedErrors = new List<Exception> { firstException, secondException, thirdException };
            observed.Do(
                errors => CollectionAssert.AreEqual(expectedErrors, errors),
                i => Assert.Fail("Result should be Failure, not Success with value of " + i));
        }

        [Test]
        public void AndTheFirstResultIsFailureThenFailureIsReturned()
        {
            var firstException = new Exception("first");
            var first = CreateFailure(firstException);
            var second = CreateSuccess(2);
            var third = CreateSuccess(4);
            int Add(int a, int b, int c) => a + b + c;

            var observed = Result.Apply(Add, first, second, third);

            var expectedErrors = new List<Exception> { firstException };
            observed.Do(
                errors => CollectionAssert.AreEqual(expectedErrors, errors),
                i => Assert.Fail("Result should be Failure, not Success with value of " + i));
        }

        [Test]
        public void AndTheSecondResultIsFailureThenFailureIsReturned()
        {
            var first = CreateSuccess(2);
            var secondException = new Exception("second");
            var second = CreateFailure(secondException);
            var third = CreateSuccess(4);
            int Add(int a, int b, int c) => a + b + c;

            var observed = Result.Apply(Add, first, second, third);

            var expectedErrors = new List<Exception> { secondException };
            observed.Do(
                errors => CollectionAssert.AreEqual(expectedErrors, errors),
                i => Assert.Fail("Result should be Failure, not Success with value of " + i));
        }

        [Test]
        public void AndTheThirdResultIsFailureThenFailureIsReturned()
        {
            var first = CreateSuccess(2);
            var second = CreateSuccess(4);
            var thirdException = new Exception("third");
            var third = CreateFailure(thirdException);
            int Add(int a, int b, int c) => a + b + c;

            var observed = Result.Apply(Add, first, second, third);

            var expectedErrors = new List<Exception> { thirdException };
            observed.Do(
                errors => CollectionAssert.AreEqual(expectedErrors, errors),
                i => Assert.Fail("Result should be Failure, not Success with value of " + i));
        }

        [Test]
        public void AndTheFirstTwoResultsAreFailuresThenFailureIsReturned()
        {
            var firstException = new Exception("first");
            var first = CreateFailure(firstException);
            var secondException = new Exception("second");
            var second = CreateFailure(secondException);
            var third = CreateSuccess(4);
            int Add(int a, int b, int c) => a + b + c;

            var observed = Result.Apply(Add, first, second, third);

            var expectedErrors = new List<Exception> { firstException, secondException };
            observed.Do(
                errors => CollectionAssert.AreEqual(expectedErrors, errors),
                i => Assert.Fail("Result should be Failure, not Success with value of " + i));
        }

        [Test]
        public void AndTheLastTwoResultsAreFailuresThenFailureIsReturned()
        {
            var first = CreateSuccess(2);
            var secondException = new Exception("second");
            var second = CreateFailure(secondException);
            var thirdException = new Exception("third");
            var third = CreateFailure(thirdException);
            int Add(int a, int b, int c) => a + b + c;

            var observed = Result.Apply(Add, first, second, third);

            var expectedErrors = new List<Exception> { secondException, thirdException };
            observed.Do(
                errors => CollectionAssert.AreEqual(expectedErrors, errors),
                i => Assert.Fail("Result should be Failure, not Success with value of " + i));
        }

        [Test]
        public void AndTheFirstAndLastResultsAreFailuresThenFailureIsReturned()
        {
            var firstException = new Exception("first");
            var first = CreateFailure(firstException);
            var second = CreateSuccess(2);
            var thirdException = new Exception("third");
            var third = CreateFailure(thirdException);
            int Add(int a, int b, int c) => a + b + c;

            var observed = Result.Apply(Add, first, second, third);

            var expectedErrors = new List<Exception> { firstException, thirdException };
            observed.Do(
                errors => CollectionAssert.AreEqual(expectedErrors, errors),
                i => Assert.Fail("Result should be Failure, not Success with value of " + i));
        }

        [Test]
        public void AndTheFirstResultIsNullThenAnExceptionIsThrown()
        {
            int Add(int a, int b, int c) => a + b + c;
            IResult<Exception, int> nullResult = null;

            Assert.Throws<ArgumentNullException>(() => Result.Apply(Add, nullResult, CreateSuccess(2), CreateSuccess(4)));
        }

        [Test]
        public void AndTheSecondResultIsNullThenAnExceptionIsThrown()
        {
            int Add(int a, int b, int c) => a + b + c;
            IResult<Exception, int> nullResult = null;

            Assert.Throws<ArgumentNullException>(() => Result.Apply(Add, CreateSuccess(2), nullResult, CreateSuccess(4)));
        }

        [Test]
        public void AndTheThirdResultIsNullThenAnExceptionIsThrown()
        {
            int Add(int a, int b, int c) => a + b + c;
            IResult<Exception, int> nullResult = null;

            Assert.Throws<ArgumentNullException>(() => Result.Apply(Add, CreateSuccess(2), CreateSuccess(4), nullResult));
        }

        private IResult<Exception, int> CreateSuccess(int i)
        {
            return Result.Success<Exception, int>(i);
        }

        private IResult<Exception, int> CreateFailure(Exception e)
        {
            return Result.Failure<Exception, int>(e);
        }
    }
}
