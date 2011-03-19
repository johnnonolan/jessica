using System;
using System.IO;
using Jessica.Factories;
using Jessica.Tests.Fakes.Models;
using NUnit.Framework;

namespace Jessica.Tests
{
    [TestFixture]
    public class ResponseFactoryExtensionsTests
    {
        private IResponseFactory _factory;

        [SetUp]
        public void SetUp()
        {
            _factory = new DefaultResponseFactory(AppDomain.CurrentDomain.BaseDirectory);
        }

        [Test]
        public void AsDownload_WithPathToExistingTextFile_ShouldReturnAResponseWithStatusCodeOk()
        {
            var response = _factory.AsDownload("../../Fakes/Files/HelloWorld.txt");

            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void AsDownload_WithPathToExistingTextFile_ShouldReturnAResponseWithCorrectContentType()
        {
            var response = _factory.AsDownload("../../Fakes/Files/HelloWorld.txt");

            Assert.That(response.ContentType, Is.EqualTo("application/octet-stream"));
        }

        [Test]
        public void AsDownload_WithPathToExistingTextFile_ShouldReturnAResponseWithContentsOfTextFile()
        {
            var response = _factory.AsDownload("../../Fakes/Files/HelloWorld.txt");

            var stream = new MemoryStream();
            response.Contents.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Contains.Substring("Hello, world! This is a download."));
        }

        [Test]
        public void AsDownload_WithPathToNonExistingTextFile_ShouldReturnAResponseWithStatusCodeNotFound()
        {
            var response = _factory.AsDownload("../../Fakes/Files/NotFound.txt");

            Assert.That(response.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void AsHtml_WithPathToExistingHtmlFile_ShouldReturnAResponseWithStatusCodeOk()
        {
            var response = _factory.AsHtml("../../Fakes/Files/FakeHtml.html");

            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void AsHtml_WithPathToExistingHtmlFile_ShouldReturnAResponseWithCorrectContentType()
        {
            var response = _factory.AsHtml("../../Fakes/Files/FakeHtml.html");

            Assert.That(response.ContentType, Is.EqualTo("text/html"));
        }

        [Test]
        public void AsHtml_WithPathToExistingHtmlFile_ShouldReturnAResponseWithContentsOfHtmlFile()
        {
            var response = _factory.AsHtml("../../Fakes/Files/FakeHtml.html");
            
            var stream = new MemoryStream();
            response.Contents.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Contains.Substring("<title>My Fake HTML</title>"));
            Assert.That(contents, Contains.Substring("<h1>Hello, from Jessica!</h1>"));
        }

        [Test]
        public void AsHtml_WithPathToNonExistingHtmlFile_ShouldReturnAResponseWithStatusCodeNotFound()
        {
            var response = _factory.AsHtml("../../Fakes/Files/NotFoundHtml.html");

            Assert.That(response.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void AsCss_WithPathToExistingCssFile_ShouldReturnAResponseWithStatusCodeOk()
        {
            var response = _factory.AsCss("../../Fakes/Files/Base.css");

            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void AsCss_WithPathToExistingCssFile_ShouldReturnAResponseWithCorrectContentType()
        {
            var response = _factory.AsCss("../../Fakes/Files/Base.css");

            Assert.That(response.ContentType, Is.EqualTo("text/css"));
        }

        [Test]
        public void AsCss_WithPathToExistingHtmlFile_ShouldReturnAResponseWithContentsOfCssFile()
        {
            var response = _factory.AsCss("../../Fakes/Files/Base.css");

            var stream = new MemoryStream();
            response.Contents.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Contains.Substring("* { margin:0; }"));
            Assert.That(contents, Contains.Substring("body { background:#ccc; }"));
        }

        [Test]
        public void AsCss_WithPathToNonExistingCssFile_ShouldReturnAResponseWithStatusCodeNotFound()
        {
            var response = _factory.AsCss("../../Fakes/Files/NotFoundCss.css");

            Assert.That(response.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void AsJs_WithPathToExistingJsFile_ShouldReturnAResponseWithStatusCodeOk()
        {
            var response = _factory.AsJs("../../Fakes/Files/SimpleJs.js");

            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void AsJs_WithPathToExistingJsFile_ShouldReturnAResponseWithCorrectContentType()
        {
            var response = _factory.AsJs("../../Fakes/Files/SimpleJs.js");

            Assert.That(response.ContentType, Is.EqualTo("text/javascript"));
        }

        [Test]
        public void AsJs_WithPathToExistingJsFile_ShouldReturnAResponseWithContentsOfJsFile()
        {
            var response = _factory.AsJs("../../Fakes/Files/SimpleJs.js");

            var stream = new MemoryStream();
            response.Contents.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Contains.Substring("var myAlert = function (a) { alert(a); }"));
        }

        [Test]
        public void AsJs_WithPathToNonExistingJsFile_ShouldReturnAResponseWithStatusCodeNotFound()
        {
            var response = _factory.AsJs("../../Fakes/Files/NotFoundJs.js");

            Assert.That(response.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void AsJson_WithModel_ShouldReturnAResponseWithStatusCodeOk()
        {
            var response = _factory.AsJson(new SimpleModel { Message = "Hello, world!", Count = 2 });

            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void AsJson_WithModel_ShouldReturnAResponseWithCorrectContentType()
        {
            var response = _factory.AsJson(new SimpleModel { Message = "Hello, world!", Count = 2 });

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void AsJson_WithModel_ShouldReturnAResponseWithContentsAsJson()
        {
            var response = _factory.AsJson(new SimpleModel { Message = "Hello, world!", Count = 2 });

            var stream = new MemoryStream();
            response.Contents.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Contains.Substring("{\"Message\":\"Hello, world!\",\"Count\":2}"));
        }

        [Test]
        public void AsRedirect_WithLocationUrl_ShouldReturnAResponseWithCorrectHeader()
        {
            var response = _factory.AsRedirect("http://google.com");

            Assert.That(response.Headers.Keys, Contains.Item("Location"));
            Assert.That(response.Headers["Location"], Is.EqualTo("http://google.com"));
        }

        [Test]
        public void AsRedirect_WithLocationUrl_ShouldReturnAResponseWithStatusCodeSeeOther()
        {
            var response = _factory.AsRedirect("http://google.com");

            Assert.That(response.StatusCode, Is.EqualTo(303));
        }

        [Test]
        public void AsText_WithSimpleString_ShouldReturnAResponseWithStatusCodeOk()
        {
            var response = _factory.AsText("Hello, world!");

            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void AsText_WithSimpleString_ShouldReturnAResponseWithCorrectContentType()
        {
            var response = _factory.AsText("Hello, world!");

            Assert.That(response.ContentType, Is.EqualTo("text/plain"));
        }

        [Test]
        public void AsText_WithSimpleString_ShouldReturnAResponseWithContentsOfString()
        {
            var response = _factory.AsText("Hello, world!");

            var stream = new MemoryStream();
            response.Contents.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Is.EqualTo("Hello, world!"));
        }

        [Test]
        public void AsText_WithStringFormat_ShouldReturnAResponseWithContentsOfFormattedString()
        {
            var date = DateTime.Now;
            var response = _factory.AsText("Hello, {0} it's {1}", "Tom", date);

            var stream = new MemoryStream();
            response.Contents.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Is.EqualTo("Hello, Tom it's " + date));
        }
    }
}
