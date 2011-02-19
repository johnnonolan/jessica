using Jessica.Tests.Data;
using NUnit.Framework;

namespace Jessica.Tests
{
    [TestFixture]
    public class JessicaModuleTests
    {
        [Test]
        public void Constructor_With2Routes_ShouldContain2Routes()
        {
            var module = new SimpleModule();

            Assert.That(module.Routes.Count, Is.EqualTo(2));
        }

        [Test]
        public void Constructor_WithRouteWithGetAndPostActions_ShouldContain2Actions()
        {
            var module = new SimpleModule();

            Assert.That(module.Routes[""].Count, Is.EqualTo(2));
        }

        [Test]
        public void Constructor_WithOverwrittenRoutes_ShouldContain1Action()
        {
            var module = new OverwrittenModule();

            Assert.That(module.Routes["overwrite"].Count, Is.EqualTo(1));
        }
    }
}
