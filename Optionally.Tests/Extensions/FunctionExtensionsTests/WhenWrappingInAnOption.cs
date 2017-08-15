using System;
using NUnit.Framework;
using Optionally.Extensions;

namespace Optionally.Tests.Extensions.FunctionExtensionsTests
{
    [TestFixture]
    public class WhenWrappingInAnOption
    {
        [Test]
        public void AndFunctionThrowsExceptionThenNoneIsReturned()
        {
            Func<int> throws = () => throw new Exception("failboat");

            var observed = throws.WrapInOption();

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndFunctionDoesntThrowThenSomeValueIsReturned()
        {
            Func<int> doesntThrow = () => 4;

            var observed = doesntThrow.WrapInOption();

            var expected = Option.Some(4);
            Assert.AreEqual(expected, observed);
        }
    }
}
