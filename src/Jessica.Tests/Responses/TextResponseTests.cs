using System;
using System.IO;
using Jessica.Responses;
using NUnit.Framework;

namespace Jessica.Tests.Responses
{
    [TestFixture]
    public class TextResponseTests
    {
        [Test]
        public void AsText_WithSimpleString_ShouldReturnAResponseWithStatusCodeOk()
        {
            var response = new TextResponse("Hello, world!");

            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void AsText_WithSimpleString_ShouldReturnAResponseWithCorrectContentType()
        {
            var response = new TextResponse("Hello, world!");

            Assert.That(response.ContentType, Is.EqualTo("text/plain"));
        }

        [Test]
        public void AsText_WithSimpleString_ShouldReturnAResponseWithContentsOfString()
        {
            var response = new TextResponse("Hello, world!");

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
            var response = new TextResponse("Hello, {0} it's {1}", "Tom", date);

            var stream = new MemoryStream();
            response.Contents.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Is.EqualTo("Hello, Tom it's " + date));
        }
    }
}
