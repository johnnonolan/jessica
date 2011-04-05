using System;
using System.Collections.Generic;

namespace Jessica.Routing
{
    public class JessicaRoute
    {
        public string Url { get; private set; }
        public string Name { get; private set; }
        public IDictionary<string, Func<dynamic, Response>> Actions { get; private set; }

        public JessicaRoute(string url, string name, IDictionary<string, Func<dynamic, Response>> actions)
        {
            Url = url;
            Name = name;
            Actions = actions;
        }
    }
}
