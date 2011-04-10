using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Jessica.Routing;
using Jessica.ViewEngines;

namespace Jessica
{
    public class JessModule
    {
        public IList<JessicaRoute> Routes { get; private set; }

        public BeforeFilters Before { get; private set; }
        public AfterFilters After { get; private set; }

        ViewFactory _viewFactory;
        string _basePath;

        protected JessModule(string basePath = "")
        {
            Routes = new List<JessicaRoute>();
            Before = new BeforeFilters();
            After = new AfterFilters();

            _viewFactory = new ViewFactory(Jess.ViewEngines, AppDomain.CurrentDomain.BaseDirectory);
            _basePath = basePath;
        }

        private void AddRoute(string method, string route, Func<dynamic, Response> action)
        {
            route = string.Concat(_basePath, Regex.Replace(route, "/:([^/]*)", "/{$1}")).TrimStart('/');

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
                Routes.Add(new JessicaRoute(route, new Dictionary<string, Func<dynamic, Response>> { { method, action } }));
            }
        }

        public void Delete(string route, Func<dynamic, Response> action)
        {
            AddRoute("DELETE", route, action);
        }

        public void Get(string route, Func<dynamic, Response> action)
        {
            AddRoute("GET", route, action);
        }

        public void Post(string route, Func<dynamic, Response> action)
        {
            AddRoute("POST", route, action);
        }

        public void Put(string route, Func<dynamic, Response> action)
        {
            AddRoute("PUT", route, action);
        }

        public Action<Stream> View(string viewName, dynamic model = null)
        {
            return _viewFactory.RenderView(viewName, model);
        }
    }
}
