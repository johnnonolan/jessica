using System;
using System.IO;
using Jessica.Configuration;
using Jessica.Responses;
using Jessica.Specs.Fakes.Models;
using Machine.Specifications;

namespace Jessica.Specs.Responses
{
    public class when_constructing_a_response
    {
        Because of = () =>
            _response = new Response();

        It should_contain_the_default_status_code = () =>
            _response.StatusCode.ShouldEqual(200);

        It should_contain_the_default_content_type = () =>
            _response.ContentType.ShouldEqual("text/html");

        It should_contain_empty_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _response.Contents.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldBeEmpty();
            }
        };

        It should_contain_an_empty_header_collection = () =>
            _response.Headers.ShouldBeEmpty();

        static Response _response;
    }

    public class when_constructing_a_response_with_implicit_integer
    {
        Because of = () =>
            _response = 304;

        It should_contain_the_correct_status_code = () =>
            _response.StatusCode.ShouldEqual(304);

        It should_contain_the_default_content_type = () =>
            _response.ContentType.ShouldEqual("text/html");

        It should_contain_empty_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _response.Contents.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldBeEmpty();
            }
        };

        It should_contain_an_empty_header_collection = () =>
            _response.Headers.ShouldBeEmpty();

        static Response _response;
    }

    public class when_constructing_a_response_with_implicit_string
    {
        Because of = () =>
            _response = "Hello, world!";

        It should_contain_the_default_status_code = () =>
            _response.StatusCode.ShouldEqual(200);

        It should_contain_the_default_content_type = () =>
            _response.ContentType.ShouldEqual("text/html");

        It should_contain_the_correct_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _response.Contents.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldContain("Hello, world!");
            }
        };

        It should_contain_an_empty_header_collection = () =>
            _response.Headers.ShouldBeEmpty();

        static Response _response;
    }

    public class when_constructing_a_response_with_implicit_action
    {
        Because of = () =>
            _response = _action;

        It should_contain_the_default_status_code = () =>
            _response.StatusCode.ShouldEqual(200);

        It should_contain_the_default_content_type = () =>
            _response.ContentType.ShouldEqual("text/html");

        It should_contain_the_correct_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _response.Contents.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldContain("Hello, world!");
            }
        };

        It should_contain_an_empty_header_collection = () =>
            _response.Headers.ShouldBeEmpty();

        static Action<Stream> _action = stream =>
        {
            var writer = new StreamWriter(stream);
            writer.Write("Hello, world!");
            writer.Flush();
        };

        static Response _response;
    }

    public class when_constructing_a_response_with_as_file_method
    {
        Because of = () =>
        {
            Jess.Configuration = new JessicaConfiguration();
            _response = Response.AsFile("../../../Fakes/Files/Download.txt");
        };

        It should_contain_the_correct_status_code = () =>
            _response.StatusCode.ShouldEqual(200);

        It should_contain_the_correct_content_type = () =>
            _response.ContentType.ShouldEqual("application/octet-stream");

        It should_contain_the_correct_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _response.Contents.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldContain("Hello, world! This is a download.");
            }
        };

        It should_contain_an_empty_header_collection = () =>
            _response.Headers.ShouldBeEmpty();

        static Response _response;
    }

    public class when_constructing_a_response_with_as_json_method
    {
        Because of = () =>
        {
            Jess.Configuration = new JessicaConfiguration();
            _response = Response.AsJson(new SimpleModel { Message = "Hello, world!", Count = 2 });
        };

        It should_contain_the_correct_status_code = () =>
            _response.StatusCode.ShouldEqual(200);

        It should_contain_the_correct_content_type = () =>
            _response.ContentType.ShouldEqual("application/json");

        It should_contain_the_correct_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _response.Contents.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldContain("{\"Message\":\"Hello, world!\",\"Count\":2,\"Price\":0}");
            }
        };

        It should_contain_an_empty_header_collection = () =>
            _response.Headers.ShouldBeEmpty();

        static Response _response;
    }

    public class when_constructing_a_response_with_as_redirect_method
    {
        Because of = () =>
        {
            Jess.Configuration = new JessicaConfiguration();
            _response = Response.AsRedirect("http://google.com");
        };

        It should_contain_the_correct_status_code = () =>
            _response.StatusCode.ShouldEqual(303);

        It should_contain_the_correct_location_header_key = () =>
            _response.Headers.Keys.ShouldContain("Location");

        It should_contain_the_correct_location_header_value = () =>
            _response.Headers["Location"].ShouldEqual("http://google.com");

        static Response _response;
    }

    public class when_constructing_a_response_with_as_html_method
    {
        Because of = () =>
        {
            Jess.Configuration = new JessicaConfiguration();
            _response = Response.AsHtml("../../../Fakes/Files/Index.html");
        };

        It should_contain_the_correct_status_code = () =>
            _response.StatusCode.ShouldEqual(200);

        It should_contain_the_correct_content_type = () =>
            _response.ContentType.ShouldEqual("text/html");

        It should_contain_the_correct_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _response.Contents.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldContain("<title>My Fake HTML</title>");
                contents.ShouldContain("<h1>Hello, from Jessica!</h1>");
            }
        };

        It should_contain_an_empty_header_collection = () =>
            _response.Headers.ShouldBeEmpty();

        static Response _response;
    }

    public class when_constructing_a_response_with_as_css_method
    {
        Because of = () =>
        {
            Jess.Configuration = new JessicaConfiguration();
            _response = Response.AsCss("../../../Fakes/Files/Stylesheet.css");
        };

        It should_contain_the_correct_status_code = () =>
            _response.StatusCode.ShouldEqual(200);

        It should_contain_the_correct_content_type = () =>
            _response.ContentType.ShouldEqual("text/css");

        It should_contain_the_correct_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _response.Contents.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldContain("* { margin:0; }");
                contents.ShouldContain("body { background:#ccc; }");
            }
        };

        It should_contain_an_empty_header_collection = () =>
            _response.Headers.ShouldBeEmpty();

        static Response _response;
    }

    public class when_constructing_a_response_with_as_js_method
    {
        Because of = () =>
        {
            Jess.Configuration = new JessicaConfiguration();
            _response = Response.AsJs("../../../Fakes/Files/SimpleJs.js");
        };

        It should_contain_the_correct_status_code = () =>
            _response.StatusCode.ShouldEqual(200);

        It should_contain_the_correct_content_type = () =>
            _response.ContentType.ShouldEqual("text/javascript");

        It should_contain_the_correct_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _response.Contents.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldContain("var myAlert = function (a) { alert(a); }");
            }
        };

        It should_contain_an_empty_header_collection = () =>
            _response.Headers.ShouldBeEmpty();

        static Response _response;
    }

    public class when_constructing_a_response_with_as_text_method_and_value
    {
        Because of = () =>
        {
            Jess.Configuration = new JessicaConfiguration();
            _response = Response.AsText("Hello, world!");
        };

        It should_contain_the_correct_status_code = () =>
            _response.StatusCode.ShouldEqual(200);

        It should_contain_the_correct_content_type = () =>
            _response.ContentType.ShouldEqual("text/plain");

        It should_contain_the_correct_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _response.Contents.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldContain("Hello, world!");
            }
        };

        It should_contain_an_empty_header_collection = () =>
            _response.Headers.ShouldBeEmpty();

        static Response _response;
    }
    
    public class when_constructing_a_response_with_as_text_method_with_format_and_arguments
    {
        Because of = () =>
        {
            Jess.Configuration = new JessicaConfiguration();
            _response = Response.AsText("Hello, {0} the number is {1}!", "Tom", 100);
        };

        It should_contain_the_correct_status_code = () =>
            _response.StatusCode.ShouldEqual(200);

        It should_contain_the_correct_content_type = () =>
            _response.ContentType.ShouldEqual("text/plain");

        It should_contain_the_correct_contents = () =>
        {
            using (var stream = new MemoryStream())
            {
                _response.Contents.Invoke(stream);
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                contents.ShouldContain("Hello, Tom the number is 100!");
            }
        };

        It should_contain_an_empty_header_collection = () =>
            _response.Headers.ShouldBeEmpty();

        static Response _response;
    }
}