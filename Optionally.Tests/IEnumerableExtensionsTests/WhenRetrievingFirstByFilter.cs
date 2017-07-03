using NUnit.Framework;
using System.Collections.Generic;

namespace Optionally.Tests.IEnumerableExtensionsTests
{
    [TestFixture]
    class WhenRetrievingFirstByFilter
    {
        [Test]
        public void AndTheListIsNullThenNoneIsReturned()
        {
            var observed = ((List<int>)null).TryFirst(_ => true);

            var expected = Option<int>.None();
            Assert.AreEqual(expected, observed);
        }
        
        [Test]
        public void AndTheFilterIsNullThenNoneIsReturned()
        {
            var observed = new List<int> { 2 }.TryFirst(null);

            var expected = Option<int>.None();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheListIsEmptyThenNoneIsReturned()
        {
            var observed = new List<int>().TryFirst(_ => true);

            var expected = Option<int>.None();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheFilterReturnsNoResultsThenNoneIsReturned()
        {
            var observed = new List<int> { 1, 2, 3 }.TryFirst(_ => false);

            var expected = Option<int>.None();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheFilterReturnsMultipleResultsThenTheFirstIsReturnedAsSome()
        {
            var observed = new List<int> { 1, 2, 3 }.TryFirst(_ => true);

            var expected = Option<int>.Some(1);
            Assert.AreEqual(expected, observed);
        }
    }
}
