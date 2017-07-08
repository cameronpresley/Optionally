using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Optionally.Tests.IEnumerableExtensionsTests
{
    [TestFixture]
    class WhenRetrievingFirstByFilter
    {
        [Test]
        public void AndTheListIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => ((List<int>) null).TryFirst(_ => true));
        }
        
        [Test]
        public void AndTheFilterIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new List<int> {2}.TryFirst(null));
        }

        [Test]
        public void AndTheListIsEmptyThenNoneIsReturned()
        {
            var observed = new List<int>().TryFirst(_ => true);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheFilterReturnsNoResultsThenNoneIsReturned()
        {
            var observed = new List<int> { 1, 2, 3 }.TryFirst(_ => false);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndTheFilterReturnsMultipleResultsThenTheFirstIsReturnedAsSome()
        {
            var observed = new List<int> { 1, 2, 3 }.TryFirst(_ => true);

            var expected = Option.Some(1);
            Assert.AreEqual(expected, observed);
        }
    }
}
