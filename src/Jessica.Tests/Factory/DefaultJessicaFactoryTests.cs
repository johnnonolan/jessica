using Jessica.Factory;
using Jessica.Tests.Data;
using NUnit.Framework;

namespace Jessica.Tests.Factory
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
        public void CreateInstance_WithSimpleModuleType_ShouldReturnInstanceOfSimpleModule()
        {
            var module = _factory.CreateInstance(typeof(SimpleModule));

            Assert.That(module, Is.InstanceOf(typeof(SimpleModule)));
        }

        [Test]
        public void CreateInstance_WithOverwrittenModuleType_ShouldReturnInstanceOfOverwrittenModule()
        {
            var module = _factory.CreateInstance(typeof(OverwrittenModule));

            Assert.That(module, Is.InstanceOf(typeof(OverwrittenModule)));
        }
    }
}
