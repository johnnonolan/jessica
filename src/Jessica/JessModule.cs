using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Jessica.Results;

namespace Jessica
{
    public class JessModule
    {
        private readonly Dictionary<string, Dictionary<string, Func<dynamic, IActionResult>>> _routes;
        private readonly BeforePipeline _beforePipeline;
        private readonly AfterPipeline _afterPipeline;

        public Dictionary<string, Dictionary<string, Func<dynamic, IActionResult>>> Routes
        {
            get { return _routes; }
        }

        public BeforePipeline Before
        {
            get { return _beforePipeline; }
        }

        public AfterPipeline After
        {
            get { return _afterPipeline; }
        }

        public JessModule()
        {
            _routes = new Dictionary<string, Dictionary<string, Func<dynamic, IActionResult>>>();
            _beforePipeline = new BeforePipeline();
            _afterPipeline = new AfterPipeline();
        }

        private void AddRouteAndAction(string name, string method, string route, Func<dynamic, IActionResult> action)
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

        protected void Delete(string name, string route, Func<dynamic, IActionResult> action)
        {
            AddRouteAndAction(name, "DELETE", route, action);
        }

        protected void Delete(string route, Func<dynamic, IActionResult> action)
        {
            Delete(null, route, action);
        }

        protected void Get(string name, string route, Func<dynamic, IActionResult> action)
        {
            AddRouteAndAction(name, "GET", route, action);
        }

        protected void Get(string route, Func<dynamic, IActionResult> action)
        {
            Get(null, route, action);
        }

        protected void Post(string name, string route, Func<dynamic, IActionResult> action)
        {
            AddRouteAndAction(name, "POST", route, action);
        }

        protected void Post(string route, Func<dynamic, IActionResult> action)
        {
            Post(null, route, action);
        }

        protected void Put(string name, string route, Func<dynamic, IActionResult> action)
        {
            AddRouteAndAction(name, "PUT", route, action);
        }

        protected void Put(string route, Func<dynamic, IActionResult> action)
        {
            Post(null, route, action);
        }
    }
}
