using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Optionally.Tests.OptionTests
{
    [TestFixture]
    public class WhenConvertingToString
    {
        [Test]
        public void AndOptionIsNoneThenNoneIsReturned()
        {
            var none = Option.No<int>();

            var observed = none.ToString();

            Assert.AreEqual("None", observed);
        }

        [Test]
        public void AndOptionIsSomeThenSomeValueIsReturned()
        {
            var some = Option.Some(5);

            var observed = some.ToString();

            Assert.AreEqual("Some of '5'", observed);
        }
    }
}
