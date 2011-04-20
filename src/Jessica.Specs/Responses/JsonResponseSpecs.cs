using System.IO;
using Jessica.Responses;
using Jessica.Specs.Fakes.Models;
using Machine.Specifications;

namespace Jessica.Specs.Responses
{
    public class JsonResponseSpecs
    {
        public class when_constructing_a_json_response_with_a_model
        {
            Establish context = () =>
                _response = new JsonResponse<SimpleModel>(new SimpleModel { Message = "Hello, world!", Count = 2 });

            It should_contain_the_correct_status_code = () =>
                _response.StatusCode.ShouldEqual(200);

            It should_contain_the_correct_content_type = () =>
                _response.ContentType.ShouldEqual("application/json");

            It should_contain_the_correct_json_contents = () =>
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

            static JsonResponse<SimpleModel> _response;
        }
    }
}
