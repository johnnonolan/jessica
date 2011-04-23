using Jessica.Responses;
using Machine.Specifications;

namespace Jessica.Specs.Responses
{
    public class when_constructing_a_redirect_response_with_a_location
    {
        Because of = () =>
            _response = new RedirectResponse("http://google.com");

        It should_contain_the_correct_status_code = () =>
            _response.StatusCode.ShouldEqual(303);

        It should_contain_the_correct_location_header_key = () =>
            _response.Headers.Keys.ShouldContain("Location");

        It should_contain_the_correct_location_header_value = () =>
            _response.Headers["Location"].ShouldEqual("http://google.com");

        static RedirectResponse _response;
    }
}
