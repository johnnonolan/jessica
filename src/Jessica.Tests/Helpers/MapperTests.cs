using System.Dynamic;
using Jessica.Helpers;
using Jessica.Tests.Data;
using NUnit.Framework;

namespace Jessica.Tests.Helpers
{
    [TestFixture]
    public class MapperTests
    {
        [Test]
        public void Map_WithExpandoAndSimpleObject_ShouldMapCorrectly()
        {
            dynamic source = new ExpandoObject();
            var destination = new SimpleObject();

            source.Message = "Hello, world!";
            source.Count = 100;

            Mapper<SimpleObject>.Map(source, destination);

            Assert.That(destination.Message, Is.EqualTo("Hello, world!"));
            Assert.That(destination.Count, Is.EqualTo(100));
        }

        [Test]
        public void Map_WithExpandoAndSimpleObjectWithLowerCasing_ShouldMapCorrectly()
        {
            dynamic source = new ExpandoObject();
            var destination = new SimpleObject();

            source.message = "Hello, world!";
            source.count = 100;

            Mapper<SimpleObject>.Map(source, destination);

            Assert.That(destination.Message, Is.EqualTo("Hello, world!"));
            Assert.That(destination.Count, Is.EqualTo(100));
        }

        [Test]
        public void Map_WithExpandoAndSimpleObjectWithDifferentCasing_ShouldMapCorrectly()
        {
            dynamic source = new ExpandoObject();
            var destination = new SimpleObject();

            source.mEsSagE = "Hello, world!";
            source.cOuNt = 100;

            Mapper<SimpleObject>.Map(source, destination);

            Assert.That(destination.Message, Is.EqualTo("Hello, world!"));
            Assert.That(destination.Count, Is.EqualTo(100));
        }

        [Test]
        public void Map_WithExpandoAndSimpleObjectWithDifferentTypes_ShouldNotMapValues()
        {
            dynamic source = new ExpandoObject();
            var destination = new SimpleObject();

            source.message = 100;
            source.count = "Hello, world!";

            Mapper<SimpleObject>.Map(source, destination);

            Assert.That(destination.Message, Is.Null);
            Assert.That(destination.Count, Is.EqualTo(0));
        }
    }
}
