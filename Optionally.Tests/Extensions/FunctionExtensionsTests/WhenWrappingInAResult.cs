using System;
using NUnit.Framework;
using Optionally.Extensions;

namespace Optionally.Tests.Extensions.FunctionExtensionsTests
{
    [TestFixture]
    public class WhenWrappingInAResult
    {
        [Test]
        public void AndTheFunctionThrowsThenAFailureIsReturned()
        {
            var exception = new Exception("failboat");
            Func<int> throws = () => throw exception;

            var result = throws.WrapInResult();

            var expected = Result<Exception, int>.Failure(exception);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndTheFunctionDoesntThrowThenASuccessIsReturned()
        {
            Func<int> doesntThrow = () => 4;

            var result = doesntThrow.WrapInResult();

            var expected = Result<Exception, int>.Success(4);
            Assert.AreEqual(expected, result);
        }
    }
}
