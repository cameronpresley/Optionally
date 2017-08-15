using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Func<int> throws = () => { throw new Exception("failboat"); };

            var result = throws.WrapInOption();

            var expected = Option.No<int>();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AndFunctionDoesntThrowThenSomeValueIsReturned()
        {
            Func<int> doesntThrow = () => 4;

            var result = doesntThrow.WrapInOption();

            var expected = Option.Some(4);
            Assert.AreEqual(expected, result);
        }
    }
}
