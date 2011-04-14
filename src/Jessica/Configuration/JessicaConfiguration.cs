using System.Configuration;

namespace Jessica.Configuration
{
    public class JessicaConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("environment", DefaultValue = "development", IsRequired = false)]
        public string Environment
        {
            get { return this["environment"].ToString().ToLower(); }
            set { this["environment"] = value; }
        }

        [ConfigurationProperty("viewsDir", DefaultValue = "views", IsRequired = false)]
        public string ViewsDirectory
        {
            get { return this["viewsDir"].ToString(); }
            set { this["viewsDir"] = value; }
        }

        public JessicaConfiguration SetEnvironment(string environment)
        {
            Environment = environment;
            return this;
        }

        public JessicaConfiguration SetViewsDirectory(string viewsDirectory)
        {
            ViewsDirectory = viewsDirectory;
            return this;
        }
    }
}
