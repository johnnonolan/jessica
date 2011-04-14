using Jessica.Configuration;
using NUnit.Framework;

namespace Jessica.Tests.Configuration
{
    [TestFixture]
    public class JessicaConfigurationTests
    {
        JessicaConfiguration _configuration;

        [SetUp]
        public void SetUp()
        {
            _configuration = new JessicaConfiguration();
        }

        [Test]
        public void Environment_WithNoSetValue_ShouldReturnDefaultValue()
        {
            Assert.That(_configuration.Environment, Is.EqualTo("development"));
        }

        [Test]
        public void ViewsDirectory_WithNoSetValue_ShouldReturnDefaultValue()
        {
            Assert.That(_configuration.ViewsDirectory, Is.EqualTo("views"));
        }

        [Test]
        public void SetEnvironment_WithEnvironment_ShouldSetCorrectValue()
        {
            _configuration.SetEnvironment("production");

            Assert.That(_configuration.Environment, Is.EqualTo("production"));
        }

        [Test]
        public void SetViewsDirectory_WithViewDirectory_ShouldSetCorrectValue()
        {
            _configuration.SetViewsDirectory("templates");

            Assert.That(_configuration.ViewsDirectory, Is.EqualTo("templates"));
        }
    }
}
