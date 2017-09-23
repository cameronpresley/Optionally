using System;
using NUnit.Framework;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    public class WhenWrapping
    {
        [Test]
        public void AndTheFuncThrowsAnExceptionThenNoneIsReturned()
        {
            int GetNumber() => throw new Exception();

            var observed = Option.Wrap(GetNumber);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheFuncReturnsAValueThenSomeValueIsReturned()
        {
            int GetNumber() => 2;

            var observed = Option.Wrap(GetNumber);

            var expected = Option.Some(2);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheFuncIsNullThenNoneIsReturned()
        {
            Func<int> nullFunc = null;

            var observed = Option.Wrap(nullFunc);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }
    }
}
