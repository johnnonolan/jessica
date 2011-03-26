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

        public static void Initialise()
        {
            RouteTable.Routes.Clear();
            NamedRoutes.Clear();

            var modules = new List<Type>();

            AppDomain.CurrentDomain.GetAssemblies().ForEach(asm => modules.AddRange(asm.GetTypes().Where(type => type.BaseType == typeof(JessModule))));

            modules.ForEach(module =>
            {
                var instance = Factory.CreateInstance(module);

                if (instance != null)
                {
                    instance.Routes.ForEach(route => RouteTable.Routes.Add(new System.Web.Routing.Route(route.Url, new JessicaRouteHandler(route.Url, module))));
                }
            });
        }
    }
}
