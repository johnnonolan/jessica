using Jessica.Results;

namespace Jessica.Tests.Data
{
    public class SimpleModule : JessModule
    {
        public SimpleModule()
        {
            Get("/",
                parameters => new StringResult("Hello, world!"));

            Post("/",
                parameters => new RedirectResult("http://google.com"));

            Get("/hello/:name",
                parameters => new StringResult(string.Format("Hello, {0}", parameters.name)));

            Get("greet", "/greet/:name",
                parameters => new StringResult("Hello, named route!"));
        }
    }
}
