using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using Jessica.Factories;
using Jessica.Routing;
using Jessica.ViewEngines;

namespace Jessica
{
    public class JessModule
    {
        public IList<Route> Routes { get; private set; }

        public BeforePipeline Before { get; private set; }

        public AfterPipeline After { get; private set; }

        public ResponseFactory Response { get; private set; }

        private ViewFactory ViewFactory { get; set; }

        protected JessModule()
        {
            Routes = new List<Route>();
            Before = new BeforePipeline();
            After = new AfterPipeline();
            Response = new ResponseFactory(AppDomain.CurrentDomain.BaseDirectory);
            ViewFactory = new ViewFactory(Jess.ViewEngines, AppDomain.CurrentDomain.BaseDirectory);
        }

        private void AddRoute(string name, string method, string route, Func<dynamic, Response> action)
        {
            route = Regex.Replace(route, "/:([^/]*)", "/{$1}").TrimStart('/');

            if (name != null)
            {
                if (Jess.NamedRoutes.ContainsKey(name))
                {
                    Jess.NamedRoutes[name] = route;
                }
                else
                {
                    Jess.NamedRoutes.Add(name, route);
                }
            }

            var existing = Routes.SingleOrDefault(r => r.Url == route);

            if (existing != null)
            {
                if (existing.Actions.ContainsKey(method))
                {
                    existing.Actions[method] = action;
                }
                else
                {
                    existing.Actions.Add(method, action);
                }
            }
            else
            {
                Routes.Add(new Route(route, new Dictionary<string, Func<dynamic, Response>> { { method, action } }));
            }
        }

        protected void Delete(string route, string name, Func<dynamic, Response> action)
        {
            AddRoute(name, "DELETE", route, action);
        }

        protected void Delete(string route, Func<dynamic, Response> action)
        {
            AddRoute(null, "DELETE", route, action);
        }

        protected void Get(string route, string name, Func<dynamic, Response> action)
        {
            AddRoute(name, "GET", route, action);
        }

        protected void Get(string route, Func<dynamic, Response> action)
        {
            AddRoute(null, "GET", route, action);
        }

        protected void Post(string route, string name, Func<dynamic, Response> action)
        {
            AddRoute(name, "POST", route, action);
        }

        protected void Post(string route, Func<dynamic, Response> action)
        {
            AddRoute(null, "POST", route, action);
        }

        protected void Put(string route, string name, Func<dynamic, Response> action)
        {
            AddRoute(name, "PUT", route, action);
        }

        protected void Put(string route, Func<dynamic, Response> action)
        {
            AddRoute(null, "PUT", route, action);
        }

        protected Action<Stream> View(string viewName, dynamic model = null)
        {
            return ViewFactory.RenderView(viewName, model);
        }

        protected Action<Stream> View(dynamic model)
        {
            return ViewFactory.RenderView(null, model);
        }
    }
}
