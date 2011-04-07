using System.Linq;
using Jessica.Tests.Fakes.Modules;
using NUnit.Framework;

namespace Jessica.Tests
{
    [TestFixture]
    public class JessModuleTests
    {
        JessModule _module;

        [SetUp]
        public void SetUp()
        {
            _module = new FakeModule();
        }

        [Test]
        public void Constructor_WithFakeModule_ShouldContainFourRoutes()
        {
            Assert.That(_module.Routes.Count, Is.EqualTo(4));
        }

        [Test]
        public void Constructor_WithRouteWithGetAndPostActions_ShouldContainTwoActions()
        {
            Assert.That(_module.Routes.Single(r => r.Url == "").Actions.Count, Is.EqualTo(2));
        }

        [Test]
        public void Constructor_WithOverwrittenRoutes_ShouldContain1Action()
        {
            Assert.That(_module.Routes.Single(r => r.Url == "overwritten").Actions.Count, Is.EqualTo(1));
        }

        [Test]
        public void Constructor_WithBasePathModule_ShouldContainRouteWithBasePathPrepended()
        {
            _module = new BasePathModule();

            Assert.That(_module.Routes.Count(r => r.Url == "base/route"), Is.EqualTo(1));
        }
    }
}
