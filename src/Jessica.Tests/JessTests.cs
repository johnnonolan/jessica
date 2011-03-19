using System.Web.Routing;
using Jessica.Factory;
using Jessica.Tests.Fakes.Factories;
using NUnit.Framework;

namespace Jessica.Tests
{
    [TestFixture]
    public class JessTests
    {
        [TearDown]
        public void TearDown()
        {
            Jess.Factory = new DefaultJessicaFactory();
        }

        [Test]
        public void Initialise_With2JessicaModules_ShouldAdd4RoutesToRouteTable()
        {
            Jess.Initialise();

            Assert.That(RouteTable.Routes.Count, Is.EqualTo(4));
        }

        [Test]
        public void Factory_WhenRequested_ShouldBeInstanceOfDefaultJessicaFactory()
        {
            Assert.That(Jess.Factory, Is.InstanceOf(typeof(DefaultJessicaFactory)));
        }

        [Test]
        public void Factory_WhenSetToCustomJessicaFactory_ShouldBeInstanceOfCustomJessicaFactory()
        {
            Jess.Factory = new CustomJessicaFactory();

            Assert.That(Jess.Factory, Is.InstanceOf(typeof(CustomJessicaFactory)));
        }
    }
}
