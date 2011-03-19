using Jessica.Extensions;
using NUnit.Framework;

namespace Jessica.Tests.Extensions
{
    public enum HttpStatusCode
    {
        OK = 200,
        Forbidden = 403,
        NotFound = 404
    }

    [TestFixture]
    public class EnumExtensionsTests
    {
        [Test]
        public void AsInt_WithEnumValue_ShouldReturnIntegerValue()
        {
            Assert.That(HttpStatusCode.OK.AsInt(), Is.EqualTo(200));
            Assert.That(HttpStatusCode.Forbidden.AsInt(), Is.EqualTo(403));
            Assert.That(HttpStatusCode.NotFound.AsInt(), Is.EqualTo(404));
        }
    }
}
