using System.IO;
using Jessica.Responses;
using Jessica.Tests.Fakes.Models;
using NUnit.Framework;

namespace Jessica.Tests.Responses
{
    [TestFixture]
    public class JsonResponseTests
    {
        [Test]
        public void Constructor_WithModel_ShouldReturnAResponseWithStatusCodeOk()
        {
            var response = new JsonResponse<SimpleModel>(new SimpleModel { Message = "Hello, world!", Count = 2 });

            Assert.That(response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void Constructor_WithModel_ShouldReturnAResponseWithCorrectContentType()
        {
            var response = new JsonResponse<SimpleModel>(new SimpleModel { Message = "Hello, world!", Count = 2 });

            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void Constructor_WithModel_ShouldReturnAResponseWithContentsAsJson()
        {
            var response = new JsonResponse<SimpleModel>(new SimpleModel { Message = "Hello, world!", Count = 2 });

            var stream = new MemoryStream();
            response.Contents.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Contains.Substring("{\"Message\":\"Hello, world!\",\"Count\":2}"));
        }
    }
}
