using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    public class WhenBiMapping
    {
        [Test]
        public void AndTheResultIsAFailureThenTheCorrectResultIsReturned()
        {
            var input = Result.Failure<Exception, int>(new Exception("Some message"));

            var actual = input.BiMap(e => e.Message, i => i);

            var expected = Result.Failure<string, int>("Some message");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AndTheResultIsAFailureThenTheFailureMapIsCalledAndNotTheSuccess()
        {
            var failureWasCalled = false;
            var successWasCalled = false;

            Func<Exception, string> failureMap = e =>
            {
                failureWasCalled = true;
                return e.Message;
            };

            Func<int, int> successMap = i =>
            {
                successWasCalled = true;
                return i;
            };

            Result
                .Failure<Exception, int>(new Exception(""))
                .BiMap(failureMap, successMap);

            Assert.That(failureWasCalled && !successWasCalled);
        }

        [Test]
        public void AndTheResultIsASuccessThenTheCorrectResultIsReturned()
        {
            var input = Result.Success<Exception, int>(5);

            var observed = input.BiMap(ex => ex.Message, num => (double) num);

            var expected = Result.Success<string, double>(5);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheResultIsASuccessThenTheSuccessMapIsCalledNotTheFailure()
        {
            var failureWasCalled = false;
            var successWasCalled = false;

            Func<Exception, string> failureMap = e =>
            {
                failureWasCalled = true;
                return e.Message;
            };
            Func<int, double> successMap = i =>
            {
                successWasCalled = true;
                return (double) i;
            };

            Result.Success<Exception, int>(5)
                .BiMap(failureMap, successMap);

            Assert.That(successWasCalled && !failureWasCalled);
        }
    }
}
