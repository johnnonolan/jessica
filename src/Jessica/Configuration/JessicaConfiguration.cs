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

        [ConfigurationProperty("publicDir", DefaultValue = "public", IsRequired = false)]
        public string PublicDirectory
        {
            get { return this["publicDir"].ToString(); }
            set { this["publicDir"] = value; }
        }

        [ConfigurationProperty("viewsDir", DefaultValue = "views", IsRequired = false)]
        public string ViewsDirectory
        {
            get { return this["viewsDir"].ToString(); }
            set { this["viewsDir"] = value; }
        }

        public bool IsProduction
        {
            get { return Environment.ToLower() == "production"; }
        }

        public bool IsDevelopment
        {
            get { return Environment.ToLower() == "development"; }
        }

        public bool IsTesting
        {
            get { return Environment.ToLower() == "testing"; }
        }

        public JessicaConfiguration SetEnvironment(string environment)
        {
            Environment = environment;
            return this;
        }

        public JessicaConfiguration SetPublicDirectory(string publicDirectory)
        {
            PublicDirectory = publicDirectory;
            return this;
        }

        public JessicaConfiguration SetViewsDirectory(string viewsDirectory)
        {
            ViewsDirectory = viewsDirectory;
            return this;
        }
    }
}
