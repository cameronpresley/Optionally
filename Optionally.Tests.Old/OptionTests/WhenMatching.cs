using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    public class WhenMatching
    {
        [Test]
        public void AndSomeThenSomeFunctionIsCalled()
        {
            int expected = 4;
            var some = Option.Some(4);

            var value = some.Match(
                ifNone: () => 0,
                ifSome: x => x);

            Assert.AreEqual(expected, value);
        }

        [Test]
        public void AndNoneThenNoneFunctionIsCalled()
        {
            int expected = 0;
            var none = Option.No<int>();
            var value = none.Match(
                ifNone: () => 0,
                ifSome: x => x);

            Assert.AreEqual(expected, value);
        }

        [Test]
        public void AndSomeAndSomeFunctionIsNullThenAnExcpetionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.Some(4).Match(() => 0, null));
        }

        [Test]
        public void AndSomeAndNoneFunctionIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.Some(4).Match(null, i => 4));
        }

        [Test]
        public void AndNoneAndSomeFunctionIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.No<int>().Match(() => 0, null));
        }

        [Test]
        public void AndNoneAndNoneFunctionIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.No<int>().Match(null, i => 4));
        }
    }
}
