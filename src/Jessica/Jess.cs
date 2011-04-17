using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Routing;
using Jessica.Extensions;
using Jessica.Factory;
using Jessica.Routing;
using Jessica.ViewEngine;

namespace Jessica
{
    public static class Jess
    {
        public static IJessicaFactory Factory { get; set; }
        public static IList<IViewEngine> ViewEngines { get; private set; }

        static Jess()
        {
            Factory = new DefaultJessicaFactory();
            ViewEngines = new List<IViewEngine>();
        }

        public static void Initialise()
        {
            RouteTable.Routes.Clear();
            ViewEngines.Clear();

            var modules = new List<Type>();
            var engines = new List<Type>();

            LoadJessicaAssemblies();

            AppDomain.CurrentDomain.GetAssemblies().ForEach(asm =>
            {
                modules.AddRange(asm.GetTypes().Where(t => t.BaseType == typeof(JessModule)));
                engines.AddRange(asm.GetTypes().Where(t => typeof(IViewEngine).IsAssignableFrom(t)).Where(t => !t.IsInterface));
            });

            RegisterRoutes(modules);
            RegisterViewEngines(engines);
        }

        private static void LoadJessicaAssemblies()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            if (Directory.Exists(path))
            {
                Directory.GetFiles(path, "Jessica*.dll").ForEach(asm => Assembly.LoadFrom(asm));
            }
        }

        private static void RegisterRoutes(IEnumerable<Type> modules)
        {
            modules.ForEach(module =>
            {
                var instance = Factory.CreateInstance(module) as JessModule;

                if (instance != null)
                {
                    instance.Routes.ForEach(route => RouteTable.Routes.Add(new Route(route.Url, new JessicaRouteHandler(route.Url, module))));
                }
            });
        }

        private static void RegisterViewEngines(IEnumerable<Type> engines)
        {
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
