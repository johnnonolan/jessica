using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Routing;
using Jessica.Configuration;
using Jessica.Extensions;
using Jessica.Factories;
using Jessica.Routing;
using Jessica.ViewEngines;

namespace Jessica
{
    public static class Jess
    {
        public static IJessicaFactory Factory { get; set; }
        public static IList<IViewEngine> ViewEngines { get; private set; }
        public static JessicaSettings Settings { get; private set; }

        static Jess()
        {
            Factory = new DefaultJessicaFactory();
            ViewEngines = new List<IViewEngine>();
        }

        public static void Initialise(JessicaSettings settings = null)
        {
            Settings = settings ?? ConfigurationManager.GetSection("jessica") as JessicaSettings;

            RouteTable.Routes.Clear();
            ViewEngines.Clear();

            var modules = new List<Type>();
            var engines = new List<Type>();

            LoadJessicaAssemblies();

            AppDomain.CurrentDomain.GetAssemblies().ForEach(asm =>
            {
                modules.AddRange(asm.GetTypes().Where(type => type.BaseType == typeof(JessModule)));
                engines.AddRange(asm.GetTypes().Where(type => typeof(IViewEngine).IsAssignableFrom(type)).Where(type => !type.IsInterface));
            });

            RegisterRoutes(modules);
            RegisterViewEngines(engines);
        }

        private static void LoadJessicaAssemblies()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            if (Directory.Exists(path))
            {
                var assemblies = Directory.GetFiles(path, "Jessica*.dll");
                assemblies.ForEach(asm => Assembly.LoadFrom(asm));
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
