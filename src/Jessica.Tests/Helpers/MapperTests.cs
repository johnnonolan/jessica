using System.Dynamic;
using Jessica.Helpers;
using Jessica.Tests.Fakes.Models;
using NUnit.Framework;

namespace Jessica.Tests.Helpers
{
    [TestFixture]
    public class MapperTests
    {
        dynamic _source;
        SimpleModel _model;

        [SetUp]
        public void SetUp()
        {
            _source = new ExpandoObject();
            _model = new SimpleModel();
        }

        [Test]
        public void Map_WithExpandoAndSimpleObject_ShouldMapCorrectly()
        {
            _source.Message = "Hello, world!";
            _source.Count = 100;

            Mapper<SimpleModel>.Map(_source, _model);

            Assert.That(_model.Message, Is.EqualTo("Hello, world!"));
            Assert.That(_model.Count, Is.EqualTo(100));
        }

        [Test]
        public void Map_WithExpandoAndSimpleObjectWithLowerCasing_ShouldMap()
        {
            _source.message = "Hello, world!";
            _source.count = 100;

            Mapper<SimpleModel>.Map(_source, _model);

            Assert.That(_model.Message, Is.EqualTo("Hello, world!"));
            Assert.That(_model.Count, Is.EqualTo(100));
        }


        [Test]
        public void Map_WithExpandoAndSimpleObjectWithDifferentCasing_ShouldMap()
        {
            _source.mEsSagE = "Hello, world!";
            _source.cOuNt = 100;

            Mapper<SimpleModel>.Map(_source, _model);

            Assert.That(_model.Message, Is.EqualTo("Hello, world!"));
            Assert.That(_model.Count, Is.EqualTo(100));
        }

        [Test]
        public void Map_WithExpandoAndSimpleObjectWithDifferentTypes_ShouldMap()
        {
            _source.message = 100;
            _source.count = "Hello, world!";

            Mapper<SimpleModel>.Map(_source, _model);

            Assert.That(_model.Message, Is.EqualTo("100"));
            Assert.That(_model.Count, Is.EqualTo(0));
        }

        [Test]
        public void Map_WithExpandoNoDestination_SHouldMapAndReturnNewObject()
        {
            _source.message = "Hello, world!";
            _source.count = 100;

            var model = Mapper<SimpleModel>.Map(_source);

            Assert.That(model.Message, Is.EqualTo("Hello, world!"));
            Assert.That(model.Count, Is.EqualTo(100));
        }

        [Test]
        public void Map_WithExpandoAndNoDestinationWithLowerCasing_ShouldMapAndReturnNewObject()
        {
            _source.message = "Hello, world!";
            _source.count = 100;

            var model = Mapper<SimpleModel>.Map(_source);

            Assert.That(model.Message, Is.EqualTo("Hello, world!"));
            Assert.That(model.Count, Is.EqualTo(100));
        }

        [Test]
        public void Map_WithExpandoAndNoDestinationWithDifferentCasing_ShouldMap()
        {
            _source.mEsSagE = "Hello, world!";
            _source.cOuNt = 100;

            var model = Mapper<SimpleModel>.Map(_source);

            Assert.That(model.Message, Is.EqualTo("Hello, world!"));
            Assert.That(model.Count, Is.EqualTo(100));
        }

        [Test]
        public void Map_WithExpandoAndNoDestinationWithDifferentTypes_ShouldMap()
        {
            _source.message = 100;
            _source.count = "Hello, world!";

            var model = Mapper<SimpleModel>.Map(_source);

            Assert.That((string)model.Message, Is.EqualTo("100"));
            Assert.That((int)model.Count, Is.EqualTo(0));
        }

        [Test]
        public void Map_WithExpandoAndNoDestinationWithDifferent_ShouldConvertAndMap()
        {
            _source.message = "Hello, world!";
            _source.count = "100";
            _source.price = "1.99";

            var model = Mapper<SimpleModel>.Map(_source);

            Assert.That((string)model.Message, Is.EqualTo("Hello, world!"));
            Assert.That((int)model.Count, Is.EqualTo(100));
            Assert.That((decimal)model.Price, Is.EqualTo(1.99));
        }
    }
}
