using System.Dynamic;
using Jessica.Helpers;
using Jessica.Tests.Fakes.Models;
using NUnit.Framework;

namespace Jessica.Tests.Helpers
{
    [TestFixture]
    public class MapperTests
    {
        private dynamic _source;
        private SimpleModel _model;

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
        public void Map_WithExpandoAndSimpleObjectWithLowerCasing_ShouldMapCorrectly()
        {
            _source.message = "Hello, world!";
            _source.count = 100;

            Mapper<SimpleModel>.Map(_source, _model);

            Assert.That(_model.Message, Is.EqualTo("Hello, world!"));
            Assert.That(_model.Count, Is.EqualTo(100));
        }

        [Test]
        public void Map_WithExpandoAndSimpleObjectWithDifferentCasing_ShouldMapCorrectly()
        {
            _source.mEsSagE = "Hello, world!";
            _source.cOuNt = 100;

            Mapper<SimpleModel>.Map(_source, _model);

            Assert.That(_model.Message, Is.EqualTo("Hello, world!"));
            Assert.That(_model.Count, Is.EqualTo(100));
        }

        [Test]
        public void Map_WithExpandoAndSimpleObjectWithDifferentTypes_ShouldNotMapValues()
        {
            _source.message = 100;
            _source.count = "Hello, world!";

            Mapper<SimpleModel>.Map(_source, _model);

            Assert.That(_model.Message, Is.Null);
            Assert.That(_model.Count, Is.EqualTo(0));
        }
    }
}
