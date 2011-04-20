using System.IO;
using Jessica.Responses;
using Machine.Specifications;

namespace Jessica.Specs.Responses
{
    public class when_constructing_a_file_response_with_existing_file_path
    {
        Establish context = () =>
            _response = new FileResponse("../../Fakes/Files/Download.txt");

        It should_contain_correct_status_code = () =>
            _response.StatusCode.ShouldEqual(200);

        It should_contain_correct_content_type = () =>
            _response.ContentType.ShouldEqual("application/octet-stream");

        It should_contain_correct_contents_of_file = () =>
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

        static FileResponse _response;
    }

    public class when_constructing_a_file_response_with_a_non_existing_file_path
    {
        Establish context = () =>
            _response = new FileResponse("FakeDownload.txt");

        It should_contain_file_not_found_status_code = () =>
            _response.StatusCode.ShouldEqual(404);

        It should_contain_the_correct_content_type = () =>
            _response.ContentType.ShouldEqual("text/html");

        static FileResponse _response;
    }
}
