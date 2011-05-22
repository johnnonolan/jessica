namespace Jessica.Specs.Fakes.Modules
{
    public class BasePathModule : JessModule
    {
        public BasePathModule()
            : base("/base")
        {
            Get("/", p => "Root of the set base path");

            Get("/route", p => "Hello, world");

            Post("/route", p => "Posted");
        }
    }
}