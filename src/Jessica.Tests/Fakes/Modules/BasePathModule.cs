namespace Jessica.Tests.Fakes.Modules
{
    public class BasePathModule : JessModule
    {
        public BasePathModule()
            : base("/base")
        {
            Get("/route", p => "Hello, world");
        }
    }
}
