using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Jessica.Filters;
using Jessica.Responses;
using Jessica.Routing;
using Jessica.ViewEngine;

namespace Jessica
{
    public class JessModule
    {
        private string _basePath;
        private ViewFactory _viewFactory;

        public JessModule(string basePath = null)
        {
            Routes = new List<JessicaRoute>();

            Before = new BeforeFilters();
            After = new AfterFilters();

            _viewFactory = new ViewFactory(Jess.ViewEngines, AppDomain.CurrentDomain.BaseDirectory);
            _basePath = basePath ?? string.Empty;
        }

        public AfterFilters After { get; set; }

        public BeforeFilters Before { get; set; }

        public IList<JessicaRoute> Routes { get; private set; }

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

        private void AddRoute(string method, string route, Func<dynamic, Response> action)
        {
            route = string.Concat(_basePath, Regex.Replace(route, "/:([^/]*)", "/{$1}")).TrimStart('/');

            var existing = Routes.SingleOrDefault(r => r.Route == route);

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
    }
}
