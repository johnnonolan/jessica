using System.Configuration;

namespace Jessica.Configuration
{
    public class JessicaSettings : ConfigurationSection
    {
        [ConfigurationProperty("environment", DefaultValue = "development", IsRequired = false)]
        public string Environment
        {
            get { return this["environment"].ToString(); }
            set { this["environment"] = value; }
        }

        [ConfigurationProperty("publicDir", DefaultValue = "public", IsRequired = false)]
        public string PublicDir
        {
            get { return this["publicDir"].ToString(); }
            set { this["publicDir"] = value; }
        }

        [ConfigurationProperty("viewsDir", DefaultValue = "views", IsRequired = false)]
        public string ViewsDir
        {
            get { return this["viewsDir"].ToString(); }
            set { this["viewsDir"] = value; }
        }

        public JessicaSettings SetEnvironment(string environment)
        {
            Environment = environment;
            return this;
        }

        public JessicaSettings SetPublicDirectory(string publicDir)
        {
            PublicDir = publicDir;
            return this;
        }

        public JessicaSettings SetViewsDirectory(string viewsDir)
        {
            ViewsDir = viewsDir;
            return this;
        }
    }
}
