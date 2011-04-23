using System.Linq;
using Jessica.Specs.Fakes.Modules;
using Machine.Specifications;

namespace Jessica.Specs
{
    public class when_constructing_a_module
    {
        Establish context = () =>
            _module = new FakeModule();

        It should_contain_the_correct_number_of_routes = () =>
            _module.Routes.Count.ShouldEqual(4);

        It should_contain_the_correct_number_of_actions_for_routes = () =>
            _module.Routes.Single(r => r.Url == "").Actions.Count.ShouldEqual(2);

        It should_contain_the_correct_number_of_actions_for_overwritten_routes = () =>
            _module.Routes.Single(r => r.Url == "overwritten").Actions.Count.ShouldEqual(1);

        static JessModule _module;
    }

    public class when_constructing_a_module_with_a_base_path_set
    {
        Establish context = () =>
            _module = new BasePathModule();

        It should_contain_the_correct_number_of_routes = () =>
            _module.Routes.Count.ShouldEqual(2);

        It should_contain_the_correct_number_of_actions_for_routes = () =>
            _module.Routes.Single(r => r.Url == "base/route").Actions.Count.ShouldEqual(2);

        It should_contain_the_correct_route_with_base_path = () =>
            _module.Routes.Count(r => r.Url == "base/route").ShouldEqual(1);

        static JessModule _module;
    }
}
