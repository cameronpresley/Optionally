using NUnit.Framework;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    class WhenFiltering
    {
        [Test]
        public void AndNoneThenNoneIsReturned()
        {
            var observed = Option<int>.None().Where(_ => true);

            var expected = Option<int>.None();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndSomeAndValueFitsFilterThenSomeReturned()
        {
            var observed = Option<int>.Some(2).Where(_ => true);

            var expected = Option<int>.Some(2);
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndSomeAndValueDoesntFitFilterThenNoneIsReturned()
        {
            var observed = Option<int>.Some(2).Where(_ => false);

            var expected = Option<int>.None();
            Assert.AreEqual(expected, observed);
        }

        [Test]
        public void AndFilterIsNullThenNoneIsReturned()
        {
            var observed = Option<int>.Some(2).Where(null);

            var expected = Option<int>.None();
            Assert.AreEqual(expected, observed);
        }
    }
}
