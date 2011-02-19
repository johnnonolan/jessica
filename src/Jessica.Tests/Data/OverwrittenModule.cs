using Jessica.Results;

namespace Jessica.Tests.Data
{
    public class OverwrittenModule : JessicaModule
    {
        public OverwrittenModule()
        {
            Get("/overwrite",
                parameters => new StringResult("You should never see me!"));

            Get("/overwrite",
                parameters => new StringResult("You should see me!"));
        }
    }
}
