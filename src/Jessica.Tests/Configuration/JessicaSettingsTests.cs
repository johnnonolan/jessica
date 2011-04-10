using System.Configuration;
using Jessica.Configuration;
using NUnit.Framework;

namespace Jessica.Tests.Configuration
{
    [TestFixture]
    public class JessicaSettingsTests
    {
        [Test]
        public void Constructor_WithNoSetValues_ShouldReturnDefaults()
        {
            var settings = new JessicaSettings();

            Assert.That(settings.Environment, Is.EqualTo("development"));
            Assert.That(settings.PublicDir, Is.EqualTo("public"));
            Assert.That(settings.ViewsDir, Is.EqualTo("views"));
        }

        [Test]
        public void SetEnvironment_WithValue_ShouldSetConfigurationSetting()
        {
            var settings = new JessicaSettings();

            settings.SetEnvironment("production");
            
            Assert.That(settings.Environment, Is.EqualTo("production"));
        }

        [Test]
        public void SetPublicDirectory_WithValue_ShouldSetConfigurationSetting()
        {
            var settings = new JessicaSettings();

            settings.SetPublicDirectory("static");

            Assert.That(settings.PublicDir, Is.EqualTo("static"));
        }

        [Test]
        public void SetViewsDirectory_WithValue_ShouldSetConfigurationSetting()
        {
            var settings = new JessicaSettings();

            settings.SetViewsDirectory("templates");

            Assert.That(settings.ViewsDir, Is.EqualTo("templates"));
        }

        [Test]
        public void Cast_WithConfigurationSection_ShouldContainCorrectSettings()
        {
            var settings = ConfigurationManager.GetSection("jessica") as JessicaSettings;

            Assert.That(settings.Environment, Is.EqualTo("production"));
            Assert.That(settings.PublicDir, Is.EqualTo("static"));
            Assert.That(settings.ViewsDir, Is.EqualTo("views"));
        }
    }
}
