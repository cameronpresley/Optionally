using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionally.Tests.ResultTests
{
    [TestFixture]
    class WhenConstructingSuccess
    {
        [Test]
        public void AndTheValueIsNullThenSuccessIsReturned()
        {
            var observed = Result<string, int>.Success(null);

            var expected = Result<string, int>.Success(null);
            Assert.AreEqual(expected, observed);
        }
    }
}
