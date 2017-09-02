using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Func<int, string> mapper = delegate (int i)
            {
                wasMapperCalled = true;
                return i.ToString();
            };

            Result.Failure<Exception, int>(new Exception()).Map(mapper);

            Assert.That(!wasMapperCalled);
        }

        [Test]
        public void AndResultIsSuccessThenSuccessIsReturned()
        {
            Func<int, string> mapper = i => i.ToString();
            var input = 2;
            var observed = Result.Success<Exception, int>(input).Map(mapper);

            var expected = Result.Success<Exception, string>(mapper(input));
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndMapperIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Result.Success<string, int>(2).Map<string>(null));
        }
    }
}
