using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Web;
using System.Web.Routing;
using Jessica.Extensions;
using Jessica.Results;

namespace Jessica.Routing
{
    public class HttpHandler : IHttpHandler
    {
        private readonly Dictionary<string, Func<dynamic, IActionResult>> _actions;

        public HttpHandler(Dictionary<string, Func<dynamic, IActionResult>> actions)
        {
            _actions = actions;
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

        private static void AddRouteDataParameters(IDictionary<string, object> parameters, HttpContextBase context)
        {
            if (context.Items.Contains("RouteData"))
            {
                var routeData = context.Items["RouteData"] as RouteData;

                if (routeData != null)
                {
                    routeData.Values.ForEach(
                        param => parameters.Add(param.Key, param.Value.ToString()));
                }
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (_actions.ContainsKey(context.Request.HttpMethod))
            {
                var wrapper = new HttpContextWrapper(context);
                IDictionary<string, object> parameters = new ExpandoObject();

                AddFormAndQueryStringParameters(parameters, wrapper.Request);
                AddRouteDataParameters(parameters, wrapper);

                var result = _actions[context.Request.HttpMethod].Invoke(parameters);
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
