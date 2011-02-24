using System;
using System.Web;
using System.Web.Routing;

namespace Jessica.Routing
{
    public class RouteHandler : IRouteHandler
    {
        private readonly string _route;
        private readonly Type _moduleType;

        public RouteHandler(string route, Type moduleType)
        {
            _route = route;
            _moduleType = moduleType;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new HttpHandler(_route, requestContext, _moduleType);
        }
    }
}
