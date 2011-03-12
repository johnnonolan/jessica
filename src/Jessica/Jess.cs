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

        public readonly static IDictionary<string, string> NamedRoutes = new Dictionary<string, string>();

        public static void Initialise()
        {
            var modules = new List<Type>();

            NamedRoutes.Clear();

            AppDomain.CurrentDomain.GetAssemblies().ForEach(
                asm => modules.AddRange(asm.GetTypes().Where(type => type.BaseType == typeof(JessicaModule))));

            modules.ForEach(module =>
            {
                var instance = Factory.CreateInstance(module);

                if (instance != null)
                {
                    instance.Routes.ForEach(
                        route => RouteTable.Routes.Add(new Route(route.Key, new RouteHandler(route.Key, module))));
                }
            });
        }
    }
}
