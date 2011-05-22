using System;
using System.Collections.Generic;
using Jessica.Responses;

namespace Jessica.Routing
{
    public class JessicaRoute
    {
        public JessicaRoute(string route, IDictionary<string, Func<dynamic, Response>> actions)
        {
            Route = route;
            Actions = actions;
        }

        public IDictionary<string, Func<dynamic, Response>> Actions { get; private set; }

        public string Route { get; private set; }
    }
}