using System.IO;
using Jessica.Responses;
using NUnit.Framework;

namespace Jessica.Tests.Responses
{
    [TestFixture]
    public class StaticFileResponseTests
    {
        [Test]
        public void Constructor_WithPathToExistingJsFile_ShouldReturnAResponseWithStatusCodeOk()
        {
            var response = new StaticFileResponse("../../Fakes/Files/SimpleJs.js", "text/javascript");

            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void Constructor_WithPathToExistingJsFile_ShouldReturnAResponseWithCorrectContentType()
        {
            var response = new StaticFileResponse("../../Fakes/Files/SimpleJs.js", "text/javascript");

            Assert.That(response.ContentType, Is.EqualTo("text/javascript"));
        }

        [Test]
        public void Constructor_WithPathToExistingJsFile_ShouldReturnAResponseWithContentsOfJsFile()
        {
            var response = new StaticFileResponse("../../Fakes/Files/SimpleJs.js", "text/javascript");

            var stream = new MemoryStream();
            response.Contents.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Contains.Substring("var myAlert = function (a) { alert(a); }"));
        }

        [Test]
        public void Constructor_WithPathToNonExistingJsFile_ShouldReturnAResponseWithStatusCodeNotFound()
        {
            var response = new StaticFileResponse("../../Fakes/Files/NotFoundJs.js", "text/javascript");

            Assert.That(response.StatusCode, Is.EqualTo(404));
        }
    }
}
