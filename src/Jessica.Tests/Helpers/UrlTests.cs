using System;
using Jessica.Helpers;
using NUnit.Framework;

namespace Jessica.Tests.Helpers
{
    [TestFixture]
    public class UrlTests
    {
        [SetUp]
        public void SetUp()
        {
            Jess.Initialise();
        }

        [Test]
        public void For_WithIncorrectName_ShouldThrowException()
        {
            Assert.Throws<Exception>(() => Url.For("generic"));
        }

        [Test]
        public void For_WithCorrectName_ShouldNotThrow()
        {
            Assert.DoesNotThrow(() => Url.For("greet"));
        }

        [Test]
        public void For_WithCorrectNameAndParameters_ShouldReturnCorrectUrl()
        {
            Assert.That(Url.For("greet", new { name = "Tom" }), Is.EqualTo("/greet/Tom"));
        }

        [Test]
        public void For_WithCorrectNameAndAdditionalParameters_ShouldReturnCorrectUrlAndQueryString()
        {
            Assert.That(Url.For("greet", new { name = "Tom", hello = "world" }), Is.EqualTo("/greet/Tom?hello=world"));
        }
    }
}
