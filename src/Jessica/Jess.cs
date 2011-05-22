using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Routing;
using Jessica.Configuration;
using Jessica.Extensions;
using Jessica.Factory;
using Jessica.Responses;
using Jessica.Routing;
using Jessica.ViewEngine;

namespace Jessica
{
    public static class Jess
    {
        static Jess()
        {
            Factory = new DefaultJessicaFactory();
            ViewEngines = new List<IViewEngine>();
        }

        public static JessicaConfiguration Configuration { get; set; }

        public static Func<Exception, RequestContext, Type, Response> ErrorHandler { get; set; }

        public static IJessicaFactory Factory { get; set; }

        public static Func<string, RequestContext, Response> NotFoundHandler { get; set; }

        public static IList<IViewEngine> ViewEngines { get; private set; }

        public static void Error(Func<Exception, RequestContext, Type, Response> errorHandler)
        {
            ErrorHandler = errorHandler;
        }

        public static void Initialise(JessicaConfiguration configuration = null)
        {
            if (Configuration == null)
            {
                Configuration = configuration ?? (JessicaConfiguration)ConfigurationManager.GetSection("jessica") ?? new JessicaConfiguration();
            }

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

            if (Configuration.IsDevelopment || NotFoundHandler != null)
            {
                RouteTable.Routes.Add(new Route("{*route}", new NotFoundRouteHandler()));
            }

            RegisterViewEngines(engines);
        }

        public static void NotFound(Func<string, RequestContext, Response> notFoundHandler)
        {
            NotFoundHandler = notFoundHandler;
        }

        private static void RegisterRoutes(IEnumerable<Type> modules)
        {
            modules.ForEach(module =>
            {
                var instance = Factory.CreateInstance(module) as JessModule;

                if (instance != null)
                {
                    instance.Routes.ForEach(route => RouteTable.Routes.Add(new Route(route.Route, new JessicaRouteHandler(route.Route, module))));
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

        private static void LoadJessicaAssemblies()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            if (Directory.Exists(path))
            {
                Directory.GetFiles(path, "Jessica*.dll").ForEach(asm => Assembly.LoadFrom(asm));
            }
        }
    }
}