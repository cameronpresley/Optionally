using NUnit.Framework;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    class WhenConstructingNone
    {
        [Test]
        public void ThenNoneIsConstructed()
        {
            var observed = Option<int>.None;

            var expected = Option<int>.None;
            Assert.AreEqual(expected, observed);
        }
    }
}
