using Jessica.Responses;

namespace Jessica.Tests.Fakes.Modules
{
    public class FakeModule : JessModule
    {
        public FakeModule()
        {
            Get("/",
                p => "Hello, world!");

            Post("/",
                p => new RedirectResponse("http://google.com"));

            Get("/hello/:name",
                p => string.Format("Hello, {0}!", p.name));

            Get("/greet/{name}",
                p => string.Format("Hello, {0}. This was a named route!", p.name));

            Get("/overwritten",
                p => "You should never see this!");

            Get("/overwritten",
                p => "You should see this!");
        }
    }
}
