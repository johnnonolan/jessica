using Jessica.Results;

namespace Jessica.Tests.Data
{
    public class SimpleModule : JessicaModule
    {
        public SimpleModule()
        {
            Get("/",
                parameters => new StringResult("Hello, world!"));

            Post("/",
                parameters => new RedirectResult("http://google.com"));

            Get("/hello/:name",
                parameters => new StringResult(string.Format("Hello, {0}", parameters.name)));
        }
    }
}
