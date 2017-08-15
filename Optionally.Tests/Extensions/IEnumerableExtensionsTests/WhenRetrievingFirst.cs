using System.Collections.Generic;
using NUnit.Framework;
using Optionally.Extensions;

namespace Optionally.Tests.Extensions.IEnumerableExtensionsTests
{
    [TestFixture]
    class WhenRetrievingFirst
    {
        [Test]
        public void AndTheListIsNullThenNoneIsReturned()
        {
            var observed = ((List<int>)null).TryFirst();

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheListHasNoElementsThenNoneIsReturned()
        {
            var observed = new List<int>().TryFirst();

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheListHasASingleElementThenSomeIsReturned()
        {
            var element = 2;

            var observed = new List<int> { element }.TryFirst();

            var expected = Option.Some(element);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheListHasMultipleElementsThenTheFirstIsReturnedAsSome()
        {
            var firstElement = 2;

            var observed = new List<int> { firstElement, 5, -10 }.TryFirst();

            var expected = Option.Some(firstElement);
            Assert.AreEqual(expected, observed);
        }
    }
}
