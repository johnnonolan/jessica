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
    public enum Env
    {
        Production,
        Development,
        Test
    }

    public static class Jess
    {
        public static IJessicaFactory Factory { get; set; }
        public static IList<IViewEngine> ViewEngines { get; private set; }

        static Jess()
        {
            Factory = new DefaultJessicaFactory();
            ViewEngines = new List<IViewEngine>();
        }

        public static void Initialise(Env environment = Env.Development)
        {
            RouteTable.Routes.Clear();

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
                    instance.Routes.ForEach(route => RouteTable.Routes.Add(new Route(route.Url, new JessicaRouteHandler(route.Url, module))));
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
        }
    }
}
