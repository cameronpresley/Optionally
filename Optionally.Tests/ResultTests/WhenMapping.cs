using NUnit.Framework;
using System;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    class WhenMapping
    {
        [Test]
        public void AndResultIsFailureThenFailureIsReturned()
        {
            var failure = new Exception();
            var observed = Result.Failure<Exception, int>(failure).Map(x => x.ToString());

            var expected = Result.Failure<Exception, string>(failure);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndResultIsFailureThenMapperIsntCalled()
        {
            var wasMapperCalled = false;

            string Mapper(int i)
            {
                wasMapperCalled = true;
                return i.ToString();
            }

            Result.Failure<Exception, int>(new Exception()).Map(Mapper);

            Assert.That(!wasMapperCalled);
        }

        [Test]
        public void AndResultIsSuccessThenSuccessIsReturned()
        {
            string Mapper(int i) => i.ToString();
            var input = 2;
            var observed = Result.Success<Exception, int>(input).Map(Mapper);

            var expected = Result.Success<Exception, string>(Mapper(input));
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndSuccessAndMapperIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Result.Success<string, int>(2).Map<string>(null));
        }

        [Test]
        public void AndFailureAndMapperIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Result.Failure<string, int>("failure").Map<string>(null));
        }
    }
}
