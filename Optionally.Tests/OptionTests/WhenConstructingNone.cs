using NUnit.Framework;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    class WhenConstructingNone
    {
        [Test]
        public void ThenNoneIsConstructed()
        {
            var observed = Option.No<int>();

            var expected = Option.No<int>();
            Assert.AreEqual(expected, observed);
        }
    }
}
