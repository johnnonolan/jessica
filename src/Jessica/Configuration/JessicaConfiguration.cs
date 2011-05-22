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

        public bool IsDevelopment
        {
            get { return Environment.ToLower() == "development"; }
        }

        public bool IsProduction
        {
            get { return Environment.ToLower() == "production"; }
        }

        public bool IsTesting
        {
            get { return Environment.ToLower() == "testing"; }
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
    }
}