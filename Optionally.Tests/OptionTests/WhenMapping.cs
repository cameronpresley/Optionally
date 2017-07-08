using NUnit.Framework;
using System;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    class WhenMapping
    {
        [Test]
        public void AndTheOptionIsNoneThenNoneIsReturned()
        {
            var observed = Option.No<int>().Map(x => x + 2);

            var expected = Option.No<int>();;

            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheOptionIsNoneThenMapIsntCalled()
        {
            var mapperWasCalled = false;
            Option.No<int>().Map(delegate (int x)
            {
                mapperWasCalled = true;
                return x + 2;
            });

            Assert.That(!mapperWasCalled);
        }

        [Test]
        public void AndTheOptionSomeThenSomeIsReturned()
        {
            var input = 4;
            Func<int, string> mapper = i => i.ToString();

            var observed = Option.Some(input).Map(mapper);

            var expected = Option.Some(mapper(input));
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheMapperIsNullThenNoneIsReturned()
        {
            var observed = Option.Some(2).Map<string>(null);

            var expected = Option.No<string>();
            Assert.AreEqual(expected, observed);
        }
    }
}
