using System.Linq;
using Jessica.Tests.Fakes.Modules;
using NUnit.Framework;

namespace Jessica.Tests
{
    [TestFixture]
    public class JessModuleTests
    {
        private JessModule _module;

        [SetUp]
        public void SetUp()
        {
            _module = new FakeModule();
        }

        [Test]
        public void Constructor_With4Routes_ShouldContain4Routes()
        {
            Assert.That(_module.Routes.Count, Is.EqualTo(4));
        }

        [Test]
        public void Constructor_WithRouteWithGetAndPostActions_ShouldContain2Actions()
        {
            Assert.That(_module.Routes.Any(r => r.Url == ""), Is.True);
            Assert.That(_module.Routes.Single(r => r.Url == "").Actions.Count, Is.EqualTo(2));
        }

        [Test]
        public void Constructor_WithOverwrittenRoutes_ShouldContain1Action()
        {
            Assert.That(_module.Routes.Any(r => r.Url == "overwritten"), Is.True);
            Assert.That(_module.Routes.Single(r => r.Url == "overwritten").Actions.Count, Is.EqualTo(1));
        }

        [Test]
        public void Constructor_WithSimpleModuleWith1NamedRoute_ShouldAdd1NamedRoute()
        {
            Assert.That(Jess.NamedRoutes.Count, Is.EqualTo(1));
        }
    }
}
