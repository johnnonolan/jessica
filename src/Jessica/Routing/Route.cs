using System;
using System.Collections.Generic;

namespace Jessica.Routing
{
    public class Route
    {
        public string Url { get; private set; }
        public IDictionary<string, Func<dynamic, Response>> Actions { get; private set; }

        public Route(string url, IDictionary<string, Func<dynamic, Response>> actions)
        {
            Url = url;
            Actions = actions;
        }
    }
}
