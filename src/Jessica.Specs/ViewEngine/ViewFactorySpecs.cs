using System;
using System.IO;
using Jessica.Specs.Fakes.Models;
using Jessica.Specs.Fakes.ViewEngine;
using Jessica.ViewEngine;
using Machine.Specifications;

namespace Jessica.Specs.ViewEngine
{
    public class when_rendering_a_view_with_null_view_name_and_model
    {
        Establish context = () =>
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            var engines = new[] { new FakeViewEngine() };
            _viewFactory = new ViewFactory(engines, rootPath);
        };

        Because of = () =>
            _exception = Catch.Exception(() => _viewFactory.RenderView(null, null));

        It should_throw_argument_exception = () =>
            _exception.ShouldBeOfType<ArgumentException>();

        static Exception _exception;
        static ViewFactory _viewFactory;
    }

    public class when_rendering_a_view_with_empty_view_name_and_null_model
    {
        Establish context = () =>
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            var engines = new[] { new FakeViewEngine() };
            _viewFactory = new ViewFactory(engines, rootPath);
        };

        Because of = () =>
            _exception = Catch.Exception(() => _viewFactory.RenderView(string.Empty, null));

        It should_throw_argument_exception = () =>
            _exception.ShouldBeOfType<ArgumentException>();

        static Exception _exception;
        static ViewFactory _viewFactory;
    }

    public class when_rendering_a_view_with_view_name_and_null_model
    {
        Establish context = () =>
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            var engines = new[] { new FakeViewEngine() };
            _viewFactory = new ViewFactory(engines, rootPath);
        };

        Because of = () =>
            _view = _viewFactory.RenderView("View", null);

        It should_contain_correct_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _view.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldContain("<title>My View!</title>");
                contents.ShouldContain("<h1>My View!</h1>");
            }
        };

        static Action<Stream> _view;
        static ViewFactory _viewFactory;
    }

    public class when_rendering_a_view_with_view_name_in_sub_directory
    {
        Establish context = () =>
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            var engines = new[] { new FakeViewEngine() };
            _viewFactory = new ViewFactory(engines, rootPath);
        };

        Because of = () =>
            _view = _viewFactory.RenderView("Shared/List", null);

        It should_contain_correct_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _view.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldContain("<title>My Sub Folder View!</title>");
                contents.ShouldContain("<h1>My Sub Folder View!</h1>");
            }
        };

        static Action<Stream> _view;
        static ViewFactory _viewFactory;
    }

    public class when_rendering_a_view_with_a_view_name_which_does_not_exist
    {
        Establish context = () =>
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            var engines = new[] { new FakeViewEngine() };
            _viewFactory = new ViewFactory(engines, rootPath);
        };

        Because of = () =>
            _view = _viewFactory.RenderView("DoesNotExist", null);

        It should_contain_correct_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _view.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldBeEmpty();
            }
        };

        static Action<Stream> _view;
        static ViewFactory _viewFactory;
    }

    public class when_rendering_a_view_with_a_null_view_name_and_model
    {
        Establish context = () =>
        {
            var rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Fakes");
            var engines = new[] { new FakeViewEngine() };
            _viewFactory = new ViewFactory(engines, rootPath);
        };

        Because of = () =>
            _view = _viewFactory.RenderView(null, new SimpleModel());

        It should_contain_correct_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _view.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldContain("<title>My Model View!</title>");
                contents.ShouldContain("<h1>My Model View!</h1>");
            }
        };

        static Action<Stream> _view;
        static ViewFactory _viewFactory;
    }
}
