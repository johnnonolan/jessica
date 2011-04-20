using System;
using System.IO;
using Jessica.ViewEngine;
using Machine.Specifications;

namespace Jessica.Specs.ViewEngine
{
    public class view_locator_setup
    {
        Establish context = () =>
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            ViewLocator = new ViewLocator(rootPath);
        };

        protected static ViewLocator ViewLocator;
    }

    public class when_locating_a_view_with_a_null_view_name : view_locator_setup
    {
        Because of = () =>
            _location = ViewLocator.FindView(null, null);

        It should_return_a_null_view_location = () =>
            _location.ShouldBeNull();

        static ViewLocation _location;
    }

    public class when_locating_a_view_with_a_view_name_and_empty_extensions_collection : view_locator_setup
    {
        Because of = () =>
            _location = ViewLocator.FindView("View", new string[] { });

        It should_return_a_null_view_location = () =>
            _location.ShouldBeNull();

        static ViewLocation _location;
    }

    public class when_locating_a_view_with_non_existing_view_name_and_extensions : view_locator_setup
    {
        Because of = () =>
            _location = ViewLocator.FindView("DoesNotExist", new[] { "html" });

        It should_return_a_null_view_location = () =>
            _location.ShouldBeNull();

        static ViewLocation _location;
    }

    public class when_locating_a_view_with_view_name : view_locator_setup
    {
        Because of = () =>
            _location = ViewLocator.FindView("View", new[] { "html" });

        It should_return_an_instance_of_view_location = () =>
            _location.ShouldNotBeNull();

        It should_contain_the_correct_location_of_view = () =>
            _location.Location.ShouldEqual(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes\\Views", "View.html"));

        It should_contain_the_correct_extension_of_view = () =>
            _location.Extension.ShouldEqual("html");

        It should_contain_the_correct_contents_of_view = () =>
        {
            var contents = _location.Contents.ReadToEnd();
            contents.ShouldContain("<title>My View!</title>");
            contents.ShouldContain("<h1>My View!</h1>");
        };

        static ViewLocation _location;
    }

    public class when_locating_a_view_with_non_existing_view_name_in_sub_directory : view_locator_setup
    {
        Because of = () =>
            _location = ViewLocator.FindView("Shared/DoesNotExist", new[] { "html" });

        It should_return_a_null_view_location = () =>
            _location.ShouldBeNull();

        static ViewLocation _location;
    }

    public class when_locating_a_view_with_view_name_in_sub_directory : view_locator_setup
    {
        Because of = () =>
            _location = ViewLocator.FindView("Shared/List", new[] { "html" });

        It should_return_an_instance_of_view_location = () =>
            _location.ShouldNotBeNull();

        It should_contain_the_correct_location_of_view = () =>
            _location.Location.ShouldEqual(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes\\Views", "Shared/List.html"));

        It should_contain_the_correct_extension_of_view = () =>
            _location.Extension.ShouldEqual("html");

        It should_contain_the_correct_contents_of_view = () =>
        {
            var contents = _location.Contents.ReadToEnd();
            contents.ShouldContain("<title>My Sub Folder View!</title>");
            contents.ShouldContain("<h1>My Sub Folder View!</h1>");
        };

        static ViewLocation _location;
    }
}
