﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Jessica.Filters;
using Jessica.Responses;
using Jessica.Routing;

namespace Jessica
{
    public abstract class JessModule
    {
        private string _basePath;

        protected JessModule(string basePath = null)
        {
            After = new AfterFilters();
            Before = new BeforeFilters();

            Routes = new List<JessicaRoute>();

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

        public Action<Stream> Render(string template, dynamic model = null)
        {
            return Jess.Render(template, model);
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