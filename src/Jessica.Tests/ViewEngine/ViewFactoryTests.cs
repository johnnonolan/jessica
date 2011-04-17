using System;
using System.IO;
using Jessica.Tests.Fakes.Models;
using Jessica.Tests.Fakes.ViewEngine;
using Jessica.ViewEngine;
using NUnit.Framework;

namespace Jessica.Tests.ViewEngine
{
    [TestFixture]
    public class ViewFactoryTests
    {
        ViewFactory _factory;

        [SetUp]
        public void SetUp()
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            var engines = new[] { new FakeViewEngine() };
            _factory = new ViewFactory(engines, rootPath);
        }

        [Test]
        public void RenderView_WithNullViewNameAndNullModel_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _factory.RenderView(null, null));
        }

        [Test]
        public void RenderView_WithEmptyViewNameAndNullModel_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _factory.RenderView("", null));
        }

        [Test]
        public void RenderView_WithViewNameAndNullModel_ShouldReturnContentsOfView()
        {
            var view = _factory.RenderView("View", null);

            var stream = new MemoryStream();
            view.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Contains.Substring("<title>My View!</title>"));
            Assert.That(contents, Contains.Substring("<h1>My View!</h1>"));
        }

        [Test]
        public void RenderView_WithViewNameInSubFolder_ShouldReturnContentsOfView()
        {
            var view = _factory.RenderView("Shared/List", null);

            var stream = new MemoryStream();
            view.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Contains.Substring("<title>My Sub Folder View!</title>"));
            Assert.That(contents, Contains.Substring("<h1>My Sub Folder View!</h1>"));
        }

        [Test]
        public void RenderView_WithNonExistingViewNameAndNullModel_ShouldReturnEmptyStream()
        {
            var view = _factory.RenderView("DoesNotExist", null);

            var stream = new MemoryStream();
            view.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Is.Empty);
        }

        [Test]
        public void RenderView_WithNullViewNameAndModel_ShouldReturnContentsOfViewNamedAfterModel()
        {
            var model = new SimpleModel();
            var view = _factory.RenderView(null, model);

            var stream = new MemoryStream();
            view.Invoke(stream);
            stream.Position = 0;
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();

            Assert.That(contents, Contains.Substring("<title>My Model View!</title>"));
            Assert.That(contents, Contains.Substring("<h1>My Model View!</h1>"));
        }
    }
}
