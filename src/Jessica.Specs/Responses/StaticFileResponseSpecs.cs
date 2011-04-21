using System.IO;
using Jessica.Responses;
using Machine.Specifications;

namespace Jessica.Specs.Responses
{
    public class when_constructing_a_static_file_response_with_file_path_and_content_type
    {
        Establish context = () =>
            _response = new StaticFileResponse("../../Fakes/Files/SimpleJs.js", "text/javascript");

        It should_contain_the_correct_status_code = () =>
            _response.StatusCode.ShouldEqual(200);

        It should_contain_the_correct_content_type = () =>
            _response.ContentType.ShouldEqual("text/javascript");

        It should_contain_the_correct_body_from_the_file = () =>
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

        static StaticFileResponse _response;
    }

    public class when_constructing_a_static_file_response_with_non_existing_file_path_and_content_type
    {
        Establish context = () =>
            _response = new StaticFileResponse("FakeJavascript.js", "text/javascript");

        It should_contain_file_not_found_status_code = () =>
            _response.StatusCode.ShouldEqual(404);

        It should_contain_the_correct_content_type = () =>
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

        static StaticFileResponse _response;
    }
}
