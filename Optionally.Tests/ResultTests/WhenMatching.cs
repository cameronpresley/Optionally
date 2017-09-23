using System;
using NUnit.Framework;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    public class WhenMatching
    {
        [Test]
        public void AndSuccessAndTheOnFailureIsNullThenAnExceptionIsThrown()
        {
            var success = CreateSuccess(5);

            Assert
                .Throws<ArgumentNullException>
                (() => success.Match(null, i => i.ToString()));
        }

        [Test]
        public void AndSuccessAndTheOnSuccessIsNullThenAnExceptionIsThrown()
        {
            var success = CreateSuccess(5);

            Assert
                .Throws<ArgumentNullException>
                (() => success.Match(s => s, null));
        }

        [Test]
        public void AndFailureAndTheOnFailureIsNullThenAnExceptionIsThrown()
        {
            var success = CreateFailure("kumquats");

            Assert
                .Throws<ArgumentNullException>
                (() => success.Match(null, i => i.ToString()));
        }

        [Test]
        public void AndFailureAndTheOnSuccessIsNullThenAnExceptionIsThrown()
        {
            var success = CreateFailure("kumquats");

            Assert
                .Throws<ArgumentNullException>
                (() => success.Match(s => s, null));
        }

        [Test]
        public void AndFailureThenOnFailureMethodIsCalled()
        {
            var failureWasCalled = false;
            int onFailure(string s)
            {
                failureWasCalled = true;
                return s.Length;
            }

            var successWasCalled = false;
            int onSuccess(int i)
            {
                successWasCalled = true;
                return i;
            }

            var observed = CreateFailure("kumquats")
                            .Match(onFailure, onSuccess);

            Assert.AreEqual("kumquats".Length, observed);
            Assert.That(failureWasCalled);
            Assert.That(!successWasCalled);
        }

        [Test]
        public void AndSuccessThenOnSuccessMethodWasCalled()
        {
            var failureWasCalled = false;
            int onFailure(string s)
            {
                failureWasCalled = true;
                return s.Length;
            }

            var successWasCalled = false;
            int onSuccess(int i)
            {
                successWasCalled = true;
                return i;
            }

            var observed = CreateSuccess(10)
                .Match(onFailure, onSuccess);

            Assert.AreEqual(10, observed);
            Assert.That(successWasCalled);
            Assert.That(!failureWasCalled);
        }

        private IResult<string, int> CreateSuccess(int num)
        {
            return Result.Success<string, int>(num);
        }

        private IResult<string, int> CreateFailure(string failure)
        {
            return Result.Failure<string, int>(failure);
        }
    }
}
