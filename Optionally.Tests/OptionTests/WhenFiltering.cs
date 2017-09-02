using System;
using NUnit.Framework;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    class WhenFiltering
    {
        [Test]
        public void AndNoneThenNoneIsReturned()
        {
            var observed = Option.No<int>().Where(_ => true);

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndSomeAndValueFitsFilterThenSomeReturned()
        {
            var observed = Option.Some(2).Where(_ => true);

            var expected = Option.Some(2);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndSomeAndValueDoesntFitFilterThenNoneIsReturned()
        {
            var observed = Option.Some(2).Where(_ => false);

            var expected = Option.No<int>();;
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndSomeAndFilterIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.Some(2).Where(null));
        }

        [Test]
        public void AndNoneAndFilterIsNullThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Option.No<int>().Where(null));
        }
    }
}
