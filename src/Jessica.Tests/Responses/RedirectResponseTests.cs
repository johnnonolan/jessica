using Jessica.Responses;
using NUnit.Framework;

namespace Jessica.Tests.Responses
{
    [TestFixture]
    public class RedirectResponseTests
    {
        [Test]
        public void Constructor_WithLocationUrl_ShouldReturnAResponseWithCorrectHeader()
        {
            var response = new RedirectResponse("http://google.com");

            Assert.That(response.Headers.Keys, Contains.Item("Location"));
            Assert.That(response.Headers["Location"], Is.EqualTo("http://google.com"));
        }

        [Test]
        public void Constructor_WithLocationUrl_ShouldReturnAResponseWithStatusCodeSeeOther()
        {
            var response = new RedirectResponse("http://google.com");

            Assert.That(response.StatusCode, Is.EqualTo(303));
        }
    }
}
