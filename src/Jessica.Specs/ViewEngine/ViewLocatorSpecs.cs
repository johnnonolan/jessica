using System;
using System.IO;
using Jessica.ViewEngine;
using Machine.Specifications;

namespace Jessica.Specs.ViewEngine
{
    public class when_locating_a_view_with_a_null_view_name
    {
        Establish context = () =>
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            _viewLocator = new ViewLocator(rootPath);
        };

        Because of = () =>
            _location = _viewLocator.FindView(null, null);

        It should_return_a_null_view_location = () =>
            _location.ShouldBeNull();

        static ViewLocation _location;
        static ViewLocator _viewLocator;
    }

    public class when_locating_a_view_with_a_view_name_and_empty_extensions_collection
    {
        Establish context = () =>
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            _viewLocator = new ViewLocator(rootPath);
        };

        Because of = () =>
            _location = _viewLocator.FindView("View", new string[] { });

        It should_return_a_null_view_location = () =>
            _location.ShouldBeNull();

        static ViewLocation _location;
        static ViewLocator _viewLocator;
    }

    public class when_locating_a_view_with_non_existing_view_name_and_extensions
    {
        Establish context = () =>
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            _viewLocator = new ViewLocator(rootPath);
        };

        Because of = () =>
            _location = _viewLocator.FindView("DoesNotExist", new[] { "html" });

        It should_return_a_null_view_location = () =>
            _location.ShouldBeNull();

        static ViewLocation _location;
        static ViewLocator _viewLocator;
    }

    public class when_locating_a_view_with_view_name
    {
        Establish context = () =>
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            _viewLocator = new ViewLocator(rootPath);
        };

        Because of = () =>
            _location = _viewLocator.FindView("View", new[] { "html" });

        It should_return_an_instance_of_view_location = () =>
            _location.ShouldNotBeNull();

        It should_contain_the_correct_location_of_view = () =>
            _location.Location.ShouldEqual(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes\\views", "View.html"));

        It should_contain_the_correct_extension_of_view = () =>
            _location.Extension.ShouldEqual("html");

        It should_contain_the_correct_contents_of_view = () =>
        {
            var contents = _location.Contents.ReadToEnd();
            contents.ShouldContain("<title>My View!</title>");
            contents.ShouldContain("<h1>My View!</h1>");
        };

        static ViewLocation _location;
        static ViewLocator _viewLocator;
    }

    public class when_locating_a_view_with_non_existing_view_name_in_sub_directory
    {
        Establish context = () =>
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            _viewLocator = new ViewLocator(rootPath);
        };

        Because of = () =>
            _location = _viewLocator.FindView("Shared/DoesNotExist", new[] { "html" });

        It should_return_a_null_view_location = () =>
            _location.ShouldBeNull();

        static ViewLocation _location;
        static ViewLocator _viewLocator;
    }

    public class when_locating_a_view_with_view_name_in_sub_directory
    {
        Establish context = () =>
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            _viewLocator = new ViewLocator(rootPath);
        };

        Because of = () =>
            _location = _viewLocator.FindView("Shared/List", new[] { "html" });

        It should_return_an_instance_of_view_location = () =>
            _location.ShouldNotBeNull();

        It should_contain_the_correct_location_of_view = () =>
            _location.Location.ShouldEqual(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes\\views", "Shared/List.html"));

        It should_contain_the_correct_extension_of_view = () =>
            _location.Extension.ShouldEqual("html");

        It should_contain_the_correct_contents_of_view = () =>
        {
            var contents = _location.Contents.ReadToEnd();
            contents.ShouldContain("<title>My Sub Folder View!</title>");
            contents.ShouldContain("<h1>My Sub Folder View!</h1>");
        };

        static ViewLocation _location;
        static ViewLocator _viewLocator;
    }
}
