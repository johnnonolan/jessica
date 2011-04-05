using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Jessica.Extensions;
using Jessica.Factories;
using Jessica.Routing;
using Jessica.ViewEngines;

namespace Jessica
{
    public enum Environment
    {
        Production,
        Development
    }

    public static class Jess
    {
        public static IJessicaFactory Factory { get; set; }
        public static IDictionary<string, string> NamedRoutes { get; private set; }
        public static IList<IViewEngine> ViewEngines { get; private set; }

        static Jess()
        {
            Factory = new DefaultJessicaFactory();
            NamedRoutes = new Dictionary<string, string>();
            ViewEngines = new List<IViewEngine>();
        }
        
        public static void Initialise(Environment environment = Environment.Development)
        {
            RouteTable.Routes.Clear();

            NamedRoutes.Clear();
            ViewEngines.Clear();

            var modules = new List<Type>();
            var engines = new List<Type>();

            AppDomain.CurrentDomain.GetAssemblies().ForEach(asm =>
            {
                modules.AddRange(asm.GetTypes()
                    .Where(type => type.BaseType == typeof(JessModule)));

                engines.AddRange(asm.GetTypes()
                    .Where(type => typeof(IViewEngine).IsAssignableFrom(type))
                    .Where(type => !type.IsInterface));
            });

            modules.ForEach(module =>
            {
                var instance = Factory.CreateInstance(module) as JessModule;

                if (instance != null)
                {
                    instance.Routes.ForEach(route =>
                    {
                        RouteTable.Routes.Add(new Route(route.Url, new JessicaRouteHandler(route.Url, module)));

                        if (route.Name != null)
                        {
                            NamedRoutes.Add(route.Name, route.Url);
                        }
                    });
                }
            });

            engines.ForEach(engine =>
            {
                var instance = Factory.CreateInstance(engine) as IViewEngine;

                if (instance != null)
                {
                    ViewEngines.Add(instance);
                }
            });

            if (environment == Environment.Development)
            {
                // idea 1...
                // RouteTable.Routes.Add(new Route("__jessica", new DiagnosticRouteHandler()));
                // RouteTable.Routes.Add(new Route("{*url}", new NotFoundRouteHandler()));

                // idea 2...
                // RouteTable.Routes.Add(new Route("__jessica", new InternalRouteHandler("diagnostic")));
                // RouteTable.Routes.Add(new Route("{*url}", new InternalRouteHandler("notfound")));
            }
        }
    }
}
