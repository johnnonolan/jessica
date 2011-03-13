﻿using System;
using System.Web;
using System.Web.Routing;

namespace Jessica.Routing
{
    public class JessicaRouteHandler : IRouteHandler
    {
        private readonly string _route;
        private readonly Type _moduleType;

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
