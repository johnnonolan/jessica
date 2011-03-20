using System.IO;
using Jessica.Responses;
using NUnit.Framework;

namespace Jessica.Tests.Responses
{
    [TestFixture]
    public class FileResponseTests
    {
        [Test]
        public void Constructor_WithPathWithPathToExistingTextFile_ShouldReturnAResponseWithStatusCodeOk()
        {
            var response = new FileResponse("../../Fakes/Files/HelloWorld.txt");

            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void Constructor_WithPathToExistingTextFile_ShouldReturnAResponseWithCorrectContentType()
        {
            var response = new FileResponse("../../Fakes/Files/HelloWorld.txt");

            Assert.That(response.ContentType, Is.EqualTo("application/octet-stream"));
        }

        [Test]
        public void Constructor_WithPathToExistingTextFile_ShouldReturnAResponseWithContentsOfTextFile()
        {
            var response = new FileResponse("../../Fakes/Files/HelloWorld.txt");

            var stream = new MemoryStream();
            response.Contents.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Contains.Substring("Hello, world! This is a download."));
        }

        [Test]
        public void Constructor_WithPathToNonExistingTextFile_ShouldReturnAResponseWithStatusCodeNotFound()
        {
            var response = new FileResponse("../../Fakes/Files/NotFound.txt");

            Assert.That(response.StatusCode, Is.EqualTo(404));
        }
    }
}
