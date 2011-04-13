using System;
using System.IO;
using Jessica.ViewEngines;
using NUnit.Framework;

namespace Jessica.Tests.ViewEngines
{
    [TestFixture]
    public class ViewLocatorTests
    {
        ViewLocator _locator;

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
        public void FindView_WithExistingViewNameAndExtensions_ShouldReturnCorrectLocation()
        {
            var location = _locator.FindView("View", new[] { "html" });

            Assert.That(location.Location, Is.EqualTo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes\\Views\\", "View.html")));
        }

        [Test]
        public void FindView_WithExistingViewNameAndExtensions_ShouldReturnCorrectExtension()
        {
            var location = _locator.FindView("View", new[] { "html" });

            Assert.That(location.Extension, Is.EqualTo("html"));
        }

        [Test]
        public void FindView_WithExistingViewNameAndExtensions_ShouldReturnCorrectContents()
        {
            var location = _locator.FindView("View", new[] { "html" });
            var contents = location.Contents.ReadToEnd();

            Assert.That(contents, Contains.Substring("<title>My View!</title>"));
            Assert.That(contents, Contains.Substring("<h1>My View!</h1>"));
        }

        [Test]
        public void FindView_WithNonExistingSubFolderViewNameAndExtensions_ShouldReturnNull()
        {
            var location = _locator.FindView("Shared/DoesNotExist", new[] { "html" });

            Assert.That(location, Is.Null);
        }

        [Test]
        public void FindView_WithExistingSubFolderViewNameAndExtensions_ShouldReturnInstanceOfViewLocation()
        {
            var location = _locator.FindView("Shared/List", new[] { "html" });

            Assert.That(location, Is.InstanceOf<ViewLocation>());
        }

        [Test]
        public void FindView_WithExistingSubFolderViewNameAndExtensions_ShouldReturnCorrectLocation()
        {
            var location = _locator.FindView("Shared/List", new[] { "html" });

            Assert.That(location.Location, Is.EqualTo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes\\Views\\", "Shared/List.html")));
        }

        [Test]
        public void FindView_WithExistingSubFolderViewNameAndExtensions_ShouldReturnCorrectExtension()
        {
            var location = _locator.FindView("Shared/List", new[] { "html" });

            Assert.That(location.Extension, Is.EqualTo("html"));
        }

        [Test]
        public void FindView_WithExistingSubFolderViewNameAndExtensions_ShouldReturnCorrectContents()
        {
            var location = _locator.FindView("Shared/List", new[] { "html" });
            var contents = location.Contents.ReadToEnd();

            Assert.That(contents, Contains.Substring("<title>My Sub Folder View!</title>"));
            Assert.That(contents, Contains.Substring("<h1>My Sub Folder View!</h1>"));
        }
    }
}
