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
            var observed = Option<int>.None.Map(x => x + 2);

            var expected = Option<int>.None;

            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheOptionIsNoneThenMapIsntCalled()
        {
            var mapperWasCalled = false;
            Option<int>.None.Map(delegate (int x)
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

            var observed = Option<int>.Some(input).Map(mapper);

            var expected = Option<string>.Some(mapper(input));
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheMapperIsNullThenNoneIsReturned()
        {
            var observed = Option<int>.Some(2).Map<string>(null);

            var expected = Option<string>.None;
            Assert.AreEqual(expected, observed);
        }
    }
}
