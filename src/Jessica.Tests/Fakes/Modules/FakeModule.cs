namespace Jessica.Tests.Fakes.Modules
{
    public class FakeModule : JessModule
    {
        public FakeModule()
        {
            Get("/",
                p => Response.AsText("Hello, world!"));

            Post("/",
                p => Response.AsRedirect("http://google.com"));

            Get("/hello/:name",
                p => Response.AsText("Hello, {0}!", (string)p.name));

            Get("/greet/{name}", "greet",
                p => Response.AsText("Hello, {0}. This is a named route!", (string)p.name));

            Get("/overwritten",
                p => Response.AsText("You should never see this!"));

            Get("/overwritten",
                p => Response.AsText("You should see this!"));
        }
    }
}
