using System.Configuration;
using Jessica.Configuration;
using Machine.Specifications;

namespace Jessica.Specs.Configuration
{
    public class when_getting_configuration_values_with_no_set_values
    {
        Because of = () =>
            _configuration = new JessicaConfiguration();

        It should_return_default_environment = () =>
            _configuration.Environment.ShouldEqual("development");

        It should_return_true_for_default_environment = () =>
            _configuration.IsDevelopment.ShouldBeTrue();

        It should_return_false_for_other_environments = () =>
        {
            _configuration.IsProduction.ShouldBeFalse();
            _configuration.IsTesting.ShouldBeFalse();
        };

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
        {
            _configuration.Environment = "production";
            _configuration.PublicDirectory = "static";
            _configuration.ViewsDirectory = "templates";
        };

        It should_return_set_environment = () =>
            _configuration.Environment.ShouldEqual("production");

        It should_return_true_for_set_environment = () =>
            _configuration.IsProduction.ShouldBeTrue();

        It should_return_false_for_other_environments = () =>
        {
            _configuration.IsDevelopment.ShouldBeFalse();
            _configuration.IsTesting.ShouldBeFalse();
        };

        It should_return_set_public_directory = () =>
            _configuration.PublicDirectory.ShouldEqual("static");

        It should_return_set_views_directory = () =>
            _configuration.ViewsDirectory.ShouldEqual("templates");

        static JessicaConfiguration _configuration;
    }

    public class when_setting_configuration_values_from_jessica_config_section
    {
        Because of = () =>
        {
            var fileMap = new ExeConfigurationFileMap{ ExeConfigFilename = "../../Fakes/Configuration/App.config" };
            var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            _configuration = config.GetSection("jessica") as JessicaConfiguration;
        };

        It should_return_set_environment = () =>
            _configuration.Environment.ShouldEqual("testing");

        It should_return_true_for_testing_environment = () =>
             _configuration.IsTesting.ShouldBeTrue();

        It should_return_false_for_other_environments = () =>
        {
            _configuration.IsDevelopment.ShouldBeFalse();
            _configuration.IsProduction.ShouldBeFalse();
        };

        It should_return_set_public_directory = () =>
            _configuration.PublicDirectory.ShouldEqual("static");

        It should_return_set_views_directory = () =>
            _configuration.ViewsDirectory.ShouldEqual("templates");

        static JessicaConfiguration _configuration;
    }
}
 