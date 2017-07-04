using NUnit.Framework;
using System.Collections.Generic;

namespace Optionally.Tests.IEnumerableExtensionsTests
{
    [TestFixture]
    class WhenRetrievingFirst
    {
        [Test]
        public void AndTheListIsNullThenNoneIsReturned()
        {
            var observed = ((List<int>)null).TryFirst();

            var expected = Option<int>.None;
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheListHasNoElementsThenNoneIsReturned()
        {
            var observed = new List<int>().TryFirst();

            var expected = Option<int>.None;
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheListHasASingleElementThenSomeIsReturned()
        {
            var element = 2;

            var observed = new List<int> { element }.TryFirst();

            var expected = Option<int>.Some(element);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheListHasMultipleElementsThenTheFirstIsReturnedAsSome()
        {
            var firstElement = 2;

            var observed = new List<int> { firstElement, 5, -10 }.TryFirst();

            var expected = Option<int>.Some(firstElement);
            Assert.AreEqual(expected, observed);
        }
    }
}
