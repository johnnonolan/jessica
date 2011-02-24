using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Jessica.Results;

namespace Jessica
{
    public class JessicaModule
    {
        private readonly Dictionary<string, Dictionary<string, Func<dynamic, IActionResult>>> _routes;

        public Dictionary<string, Dictionary<string, Func<dynamic, IActionResult>>> Routes
        {
            get { return _routes; }
        }

        public JessicaModule()
        {
            _routes = new Dictionary<string, Dictionary<string, Func<dynamic, IActionResult>>>();
        }

        private void AddRouteAndAction(string method, string route, Func<dynamic, IActionResult> action)
        {
            route = Regex.Replace(route, "/:([^/]*)", "/{$1}").TrimStart('/');

            if (_routes.ContainsKey(route))
            {
                if (_routes[route].ContainsKey(method))
                {
                    _routes[route][method] = action;
                }
                else
                {
                    _routes[route].Add(method, action);
                }
            }
            else
            {
                _routes.Add(route, new Dictionary<string, Func<dynamic, IActionResult>> 
                {
                    { method, action } 
                });
            }
        }

        protected void Delete(string route, Func<dynamic, IActionResult> action)
        {
            AddRouteAndAction("DELETE", route, action);
        }

        protected void Get(string route, Func<dynamic, IActionResult> action)
        {
            AddRouteAndAction("GET", route, action);
        }

        protected void Post(string route, Func<dynamic, IActionResult> action)
        {
            AddRouteAndAction("POST", route, action);
        }

        protected void Put(string route, Func<dynamic, IActionResult> action)
        {
            AddRouteAndAction("PUT", route, action);
        }
    }
}
