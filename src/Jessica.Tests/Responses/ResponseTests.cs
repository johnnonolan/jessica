using System.IO;
using Jessica.Configuration;
using Jessica.Responses;
using NUnit.Framework;

namespace Jessica.Tests.Responses
{
    [TestFixture]
    public class ResponseTests
    {
        [SetUp]
        public void SetUp()
        {
            Jess.Configuration = new JessicaConfiguration();
        }

        [TearDown]
        public void TearDown()
        {
            Jess.Configuration = null;
        }

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

        [Test]
        public void AsFile_WithFilePath_ShouldReturnResponseWithCorrectContentType()
        {
            var response = Response.AsFile("../../../Fakes/Files/HelloWorld.txt");

            Assert.That(response.ContentType, Is.EqualTo("application/octet-stream"));
        }

        [Test]
        public void AsJson_WithObject_ShouldReturnResponseWithCorrectContentType()
        {
            var response = Response.AsJson("Hello, world");

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void AsRedirect_WithLocation_ShouldReturnResponseWithCorrectHeader()
        {
            var response = Response.AsRedirect("http://google.com");

            Assert.That(response.Headers["Location"], Is.EqualTo("http://google.com"));
        }

        [Test]
        public void AsHtml_WithFilePath_ShouldReturnResponseWithCorrectContenType()
        {
            var response = Response.AsHtml("../../../Fakes/Files/FakeHtml.html");

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
        }

        [Test]
        public void AsCss_WithFilePath_ShouldReturnResponseWithCorrectContentType()
        {
            var response = Response.AsCss("../../../Fakes/Files/Base.css");

            Assert.That(response.ContentType, Is.EqualTo("text/css"));
        }

        [Test]
        public void AsJs_WithFilePath_ShouldReturnResponseWithCorrectContentType()
        {
            var response = Response.AsJs("../../../Fakes/Files/SimpleJs.js");

            Assert.That(response.ContentType, Is.EqualTo("text/javascript"));
        }

        [Test]
        public void AsText_WithStringFormat_ShouldReturnResponseWithCorrectContentType()
        {
            var response = Response.AsText("Hello, {0}", "world!");

            Assert.That(response.ContentType, Is.EqualTo("text/plain"));
        }
    }
}
