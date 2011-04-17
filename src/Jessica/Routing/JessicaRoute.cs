using System;
using System.Collections.Generic;
using Jessica.Responses;

namespace Jessica.Routing
{
    public class JessicaRoute
    {
        public string Url { get; private set; }
        public IDictionary<string, Func<dynamic, Response>> Actions { get; private set; }

        public JessicaRoute(string url, IDictionary<string, Func<dynamic, Response>> actions)
        {
            Url = url;
            Actions = actions;
        }
    }
}
