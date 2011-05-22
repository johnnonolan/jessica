using System.Web.Routing;
using Jessica.Factory;
using Jessica.Specs.Fakes.Factory;
using Machine.Specifications;

namespace Jessica.Specs
{
    public class when_initialising_jessica_with_fakes
    {
        Because of = () =>
            Jess.Initialise();

        It should_contain_five_routes_in_the_route_table = () =>
            RouteTable.Routes.Count.ShouldEqual(6);

        It should_contain_two_view_engines_in_the_view_engine_list = () =>
            Jess.ViewEngines.Count.ShouldEqual(2);

        It should_set_factory_to_default_jessica_factory = () =>
            Jess.Factory.ShouldBeOfType<DefaultJessicaFactory>();
    }

    public class when_setting_jessica_factory
    {
        Because of = () =>
            Jess.Factory = new CustomJessicaFactory();

        It should_set_the_correct_factory = () =>
            Jess.Factory.ShouldBeOfType<CustomJessicaFactory>();
    }
}