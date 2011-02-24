using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Web;
using System.Web.Routing;
using Jessica.Extensions;

namespace Jessica.Routing
{
    public class HttpHandler : IHttpHandler
    {
        private readonly string _route;
        private readonly RequestContext _request;
        private readonly Type _moduleType;

        public HttpHandler(string route, RequestContext request, Type moduleType)
        {
            _route = route;
            _request = request;
            _moduleType = moduleType;
        }

        private static void AddFormAndQueryStringParameters(IDictionary<string, object> parameters, HttpRequestBase request)
        {
            foreach (string key in request.Form)
            {
                parameters.Add(key, request.Form[key]);
            }

            foreach (string key in request.QueryString)
            {
                parameters.Add(key, request.QueryString[key]);
            }
        }

        private void AddRouteDataParameters(IDictionary<string, object> parameters)
        {
            if (_request.RouteData != null)
            {
                _request.RouteData.Values.ForEach(
                    p => parameters.Add(p.Key, p.Value));
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            var module = Jess.Factory.CreateInstance(_moduleType);
            var routes = module.Routes;

            if (routes[_route].ContainsKey(context.Request.HttpMethod))
            {
                var wrapper = new HttpContextWrapper(context);
                IDictionary<string, object> parameters = new ExpandoObject();

                parameters.Add("request", _request);

                AddFormAndQueryStringParameters(parameters, wrapper.Request);
                AddRouteDataParameters(parameters);

                var result = routes[_route][context.Request.HttpMethod].Invoke(parameters);
                result.WriteToResponse(wrapper);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
