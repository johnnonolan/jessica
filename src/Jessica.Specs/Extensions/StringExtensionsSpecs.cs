using Jessica.Extensions;
using Machine.Specifications;

namespace Jessica.Specs.Extensions
{
    public class when_formatting_a_string_with_placeholder_and_string_argument
    {
        Establish context = () =>
            _format = "Hello, {0}";

        Because of = () =>
            _result = _format.With("world!");

        It should_return_the_correct_value = () =>
            _result.ShouldEqual("Hello, world!");

        static string _format;
        static string _result;
    }

    public class when_formatting_a_string_with_placeholder_and_integer_argument
    {
        Establish context = () =>
            _format = "10 x 10 = {0}";

        Because of = () =>
            _result = _format.With(10 * 10);

        It should_return_the_correct_value = () =>
            _result.ShouldEqual("10 x 10 = 100");

        static string _format;
        static string _result;
    }

    public class when_formatting_a_string_with_no_placeholders
    {
        Establish context = () =>
            _format = "Hello, world!";

        Because of = () =>
            _result = _format.With();

        It should_return_the_original_string = () =>
            _result.ShouldEqual("Hello, world!");

        static string _format;
        static string _result;
    }
}
