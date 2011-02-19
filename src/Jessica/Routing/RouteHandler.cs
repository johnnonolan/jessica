using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Jessica.Results;

namespace Jessica.Routing
{
    public class RouteHandler : IRouteHandler
    {
        private readonly Dictionary<string, Func<dynamic, IActionResult>> _actions;

        public RouteHandler(Dictionary<string, Func<dynamic, IActionResult>> actions)
        {
            _actions = actions;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            requestContext.HttpContext.Items.Add("RouteData", requestContext.RouteData);
            return new HttpHandler(_actions);
        }
    }
}
