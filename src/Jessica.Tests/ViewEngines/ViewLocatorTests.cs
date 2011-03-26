using System;
using System.IO;
using Jessica.ViewEngines;
using NUnit.Framework;

namespace Jessica.Tests.ViewEngines
{
    [TestFixture]
    public class ViewLocatorTests
    {
        private ViewLocator _locator;

        [SetUp]
        public void SetUp()
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            _locator = new ViewLocator(rootPath);
        }

        [Test]
        public void FindView_WithNullViewName_ShouldReturnNull()
        {
            var location = _locator.FindView(null, null);

            Assert.That(location, Is.Null);
        }

        [Test]
        public void FindView_WithViewNameAndEmptyExtensionsList_ShouldReturnNull()
        {
            var location = _locator.FindView("View", new string[] { });

            Assert.That(location, Is.Null);
        }

        [Test]
        public void FindView_WithNonExistingViewNameAndExtensions_ShouldReturnNull()
        {
            var location = _locator.FindView("DoesNotExist", new[] { "html" });

            Assert.That(location, Is.Null);
        }

        [Test]
        public void FindView_WithExistingViewNameAndExtensions_ShouldReturnInstanceOfViewLocation()
        {
            var location = _locator.FindView("View", new[] { "html" });

            Assert.That(location, Is.InstanceOf<ViewLocation>());
        }

        [Test]
        public void FindView_WithExistingViewNameAndExtensions_ShouldReturnCorrectFileLocation()
        {
            var location = _locator.FindView("View", new[] { "html" });

            Assert.That(location.Location, Is.EqualTo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes\\Views\\", "View.html")));
        }

        [Test]
        public void FindView_WithExistingViewNameAndExtensions_ShouldReturnCorrectFileExtension()
        {
            var location = _locator.FindView("View", new[] { "html" });

            Assert.That(location.Extension, Is.EqualTo("html"));
        }

        [Test]
        public void FindView_WithExistingViewNameAndExtensions_ShouldReturnCorrectFileContents()
        {
            var location = _locator.FindView("View", new[] { "html" });
            var contents = location.Contents.ReadToEnd();

            Assert.That(contents, Contains.Substring("<title>My View!</title>"));
            Assert.That(contents, Contains.Substring("<h1>My View!</h1>"));
        }
    }
}
