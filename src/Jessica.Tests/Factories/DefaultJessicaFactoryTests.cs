using Jessica.Factory;
using Jessica.Tests.Fakes.Modules;
using NUnit.Framework;

namespace Jessica.Tests.Factories
{
    [TestFixture]
    public class DefaultJessicaFactoryTests
    {
        private IJessicaFactory _factory;

        [SetUp]
        public void SetUp()
        {
            _factory = new DefaultJessicaFactory();
        }

        [Test]
        public void CreateInstance_WithFakeModuleType_ShouldReturnInstanceOfFakeModule()
        {
            var module = _factory.CreateInstance(typeof(FakeModule));

            Assert.That(module, Is.InstanceOf(typeof(FakeModule)));
        }
    }
}
