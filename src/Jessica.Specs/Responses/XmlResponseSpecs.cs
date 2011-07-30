using System.IO;
using Jessica.Responses;
using Jessica.Specs.Fakes.Models;
using Machine.Specifications;

namespace Jessica.Specs.Responses
{
    public class XnkResponseSpecs
    {
        public class when_constructing_a_xml_response_with_a_model
        {
            Because of = () =>
                _response = new XmlResponse<SimpleModel>(new SimpleModel { Message = "Hello, world!", Count = 2 });

            It should_contain_the_correct_status_code = () =>
                _response.StatusCode.ShouldEqual(200);

            It should_contain_the_correct_content_type = () =>
                _response.ContentType.ShouldEqual("application/xml");

            It should_contain_the_correct_json_contents = () =>
            {
                using (var stream = new MemoryStream())
                {
                    _response.Contents.Invoke(stream);
                    stream.Position = 0;
                    var reader = new StreamReader(stream);
                    var contents = reader.ReadToEnd();

                    contents.ShouldContain("<Message>Hello, world!</Message>");
                    contents.ShouldContain("<Count>2</Count>");
                    contents.ShouldContain("<Price>0</Price>");
                }
            };

            It should_contain_an_empty_header_collection = () =>
                _response.Headers.ShouldBeEmpty();

            static XmlResponse<SimpleModel> _response;
        }
    }
}