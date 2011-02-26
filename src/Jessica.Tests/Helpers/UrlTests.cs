using System;
using Jessica.Helpers;
using Jessica.Tests.Data;
using NUnit.Framework;

namespace Jessica.Tests.Helpers
{
    [TestFixture]
    public class UrlTests
    {
        [SetUp]
        public void SetUp()
        {
            Jess.NamedRoutes.Clear();
        }

        [Test]
        public void For_WithIncorrectName_ShouldThrowException()
        {
            var module = new SimpleModule();

            Assert.Throws<Exception>(() => Url.For("generic"));
        }

        [Test]
        public void For_WithCorrectName_ShouldNotThrow()
        {
            var module = new SimpleModule();

            Assert.DoesNotThrow(() => Url.For("greet"));
        }

        [Test]
        public void For_WithCorrectNameAndParameters_ShouldReturnCorrectUrl()
        {
            var module = new SimpleModule();
            var result = Url.For("greet", new { name = "Tom" });

            Assert.That(result, Is.EqualTo("/greet/Tom"));
        }

        [Test]
        public void For_WithCorrectNameAndAdditionalParameters_ShouldReturnCorrectUrlAndQueryString()
        {
            var module = new SimpleModule();
            var result = Url.For("greet", new { name = "Tom", hello = "world" });

            Assert.That(result, Is.EqualTo("/greet/Tom?hello=world"));
        }
    }
}
