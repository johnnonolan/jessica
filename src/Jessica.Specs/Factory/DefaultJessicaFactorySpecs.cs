using Jessica.Factory;
using Jessica.Specs.Fakes.Modules;
using Machine.Specifications;

namespace Jessica.Specs.Factory
{
    public class when_creating_an_instance_of_an_object_with_default_factory
    {
        Establish context = () =>
            _factory = new DefaultJessicaFactory();

        Because of = () =>
            _module = _factory.CreateInstance(typeof(FakeModule));

        It should_not_return_null = () =>
            _module.ShouldNotBeNull();

        It should_create_an_object_of_the_correct_type = () =>
            _module.ShouldBe(typeof(FakeModule));

        static IJessicaFactory _factory;
        static object _module;
    }
}
