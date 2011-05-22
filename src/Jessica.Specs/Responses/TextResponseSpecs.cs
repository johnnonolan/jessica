using System.IO;
using Jessica.Responses;
using Machine.Specifications;

namespace Jessica.Specs.Responses
{
    public class when_constructing_a_text_response_with_value
    {
        Because of = () =>
            _response = new TextResponse("Hello, world!");

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

        static TextResponse _response;
    }

    public class when_constructing_a_text_response_with_format_and_arguments
    {
        Because of = () =>
            _response = new TextResponse("Hello, {0} the number is {1}!", "Tom", 100);

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

        static TextResponse _response;
    }
}