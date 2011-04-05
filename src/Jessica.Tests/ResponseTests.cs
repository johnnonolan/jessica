using System.IO;
using NUnit.Framework;

namespace Jessica.Tests
{
    [TestFixture]
    public class ResponseTests
    {
        [Test]
        public void Constructor_ShouldReturnAResponseWithStatusCodeOk()
        {
            var response = new Response();

            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void Constructor_ShouldReturnAResponseWithCorrectContentType()
        {
            var response = new Response();

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
        }

        [Test]
        public void Constructor_ShouldReturnAResponseWithEmptyContent()
        {
            var response = new Response();

            var stream = new MemoryStream();
            response.Contents.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Is.Empty);
        }

        [Test]
        public void Constructor_ShouldReturnAResponseWithNonNullHeaderList()
        {
            var response = new Response();

            Assert.That(response.Headers, Is.Not.Null);
        }

        [Test]
        public void Implicit_WithStatusCode_ShouldReturnAResponseWithCorrectStatusCode()
        {
            Response response = 200;

            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void Implicit_WithStringValue_ShouldReturnAResponseWithCOrrectContents()
        {
            Response response = "Hello, world!";

            var stream = new MemoryStream();
            response.Contents.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Is.EqualTo("Hello, world!"));
        }
    }
}
