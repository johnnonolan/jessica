using System.Configuration;
using Jessica.Configuration;
using Machine.Specifications;

namespace Jessica.Specs.Configuration
{
    public class when_getting_configuration_values_with_no_set_values
    {
        Establish context = () =>
            _configuration = new JessicaConfiguration();

        It should_return_default_environment = () =>
            _configuration.Environment.ShouldEqual("development");

        It should_return_default_public_directory = () =>
            _configuration.PublicDirectory.ShouldEqual("public");

        It should_return_default_views_directory = () =>
            _configuration.ViewsDirectory.ShouldEqual("views");

        static JessicaConfiguration _configuration;
    }

    public class when_setting_configuration_values
    {
        Establish context = () =>
            _configuration = new JessicaConfiguration();

        Because of = () =>
            _configuration
                .SetEnvironment("production")
                .SetPublicDirectory("static")
                .SetViewsDirectory("templates");

        It should_return_set_environment = () =>
            _configuration.Environment.ShouldEqual("production");

        It should_return_set_public_directory = () =>
            _configuration.PublicDirectory.ShouldEqual("static");

        It should_return_set_views_directory = () =>
            _configuration.ViewsDirectory.ShouldEqual("templates");

        static JessicaConfiguration _configuration;
    }

    public class when_setting_configuration_values_from_jessica_config_section
    {
        Establish context = () =>
        {
            _configuration = ConfigurationManager.GetSection("jessica") as JessicaConfiguration;
        };

        It should_return_set_environment;

        It should_return_set_public_directory;

        It should_return_set_views_directory;

        static JessicaConfiguration _configuration;
    }
}
