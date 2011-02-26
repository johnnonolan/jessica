using Jessica.Tests.Data;
using NUnit.Framework;

namespace Jessica.Tests
{
    [TestFixture]
    public class JessicaModuleTests
    {
        [Test]
        public void Constructor_With3Routes_ShouldContain3Routes()
        {
            var module = new SimpleModule();

            Assert.That(module.Routes.Count, Is.EqualTo(3));
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

        [Test]
        public void Constructor_WithSimpleModuleWith1NamedRoute_ShouldAdd1NamedRoute()
        {
            var module = new SimpleModule();

            Assert.That(Jess.NamedRoutes.Count, Is.EqualTo(1));
        }
    }
}
