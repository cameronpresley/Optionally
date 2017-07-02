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
            var observed = Result<int, Exception>.Failure(failure).Map(x => x.ToString());

            var expected = Result<string, Exception>.Failure(failure);
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

            Result<int, Exception>.Failure(new Exception()).Map(mapper);

            Assert.That(!wasMapperCalled);
        }

        [Test]
        public void AndResultIsSuccessThenSuccessIsReturned()
        {
            Func<int, string> mapper = i => i.ToString();
            var input = 2;
            var observed = Result<int, Exception>.Success(input).Map(mapper);

            var expected = Result<string, Exception>.Success(mapper(input));
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndMapperIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Result<int, string>.Success(2).Map<string>(null));
        }
    }
}
