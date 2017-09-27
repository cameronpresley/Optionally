using NUnit.Framework;
using System;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    class WhenMapping
    {
        [Test]
        public void AndNoneThenNoneIsReturned()
        {
            var observed = Option.No<int>().Map(x => x + 2);

            var expected = Option.No<int>();

            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndNoneThenMapIsntCalled()
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
            int Square(int i) => i * i;

            var observed = Option.Some(input).Map(Square);

            var expected = Option.Some(Square(input));
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheMapperIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.Some(2).Map<string>(null));
        }

        [Test]
        public void AndNoneAndMapperIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.No<int>().Map<string>(null));
        }
    }
}
