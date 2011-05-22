using System;
using System.Web;
using System.Web.Routing;

namespace Jessica.Routing
{
    public class JessicaRouteHandler : IRouteHandler
    {
        private Type _moduleType;
        private string _route;

        public JessicaRouteHandler(string route, Type moduleType)
        {
            _route = route;
            _moduleType = moduleType;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new JessicaHttpHandler(_route, requestContext, _moduleType);
        }
    }
}