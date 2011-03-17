using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Jessica.Extensions;
using Jessica.Factory;
using Jessica.Routing;

namespace Jessica
{
    public static class Jess
    {
        public static IJessicaFactory Factory = new DefaultJessicaFactory();

        public static IDictionary<string, string> NamedRoutes = new Dictionary<string, string>();

        public static void Initialise()
        {
            NamedRoutes.Clear();

            var modules = new List<Type>();

            AppDomain.CurrentDomain.GetAssemblies().ForEach(
                asm => modules.AddRange(asm.GetTypes().Where(type => type.BaseType == typeof(JessModule))));

            modules.ForEach(module =>
            {
                var instance = Factory.CreateInstance(module);

                if (instance != null)
                {
                    instance.Routes.ForEach(route => RouteTable.Routes.Add(new Route(route.Key, new JessicaRouteHandler(route.Key, module))));
                }
            });
        }
    }
}
