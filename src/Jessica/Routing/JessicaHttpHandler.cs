using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using Jessica.Extensions;
using Jessica.Responses;

namespace Jessica.Routing
{
    public class JessicaHttpHandler : IHttpHandler, IRequiresSessionState
    {
        Type _moduleType;
        string _route;
        RequestContext _requestContext;

        public JessicaHttpHandler(string route, RequestContext requestContext, Type moduleType)
        {
            _route = route;
            _requestContext = requestContext;
            _moduleType = moduleType;
        }

        public bool IsReusable 
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            InvokeRequestLifeCycle(context);
        }

        private void InvokeRequestLifeCycle(HttpContext context)
        {
            var module = Jess.Factory.CreateInstance(_moduleType) as JessModule;

            if (module != null)
            {
                var response = InvokeBeforeFilters(module) ?? ResolveAndInvokeRoute(module, context);
                MapResponseToHttpResponse(response, context.Response);

                if (module.After != null)
                {
                    module.After.Invoke(_requestContext);
                }
            }
            else
            {
                MapResponseToHttpResponse(500, context.Response);
            }
        }

        private Response InvokeBeforeFilters(JessModule module)
        {
            return module.Before != null ? module.Before.Invoke(_requestContext) : null;
        }

        private Response ResolveAndInvokeRoute(JessModule module, HttpContext context)
        {
            var route = module.Routes.Single(r => r.Route == _route);
            var method = context.Request.HttpMethod.ToUpper();
            return route.Actions.ContainsKey(method) ? route.Actions[method].Invoke(BuildParameterObject(context)) : 405;
        }

        private dynamic BuildParameterObject(HttpContext context)
        {
            IDictionary<string, object> parameters = new ExpandoObject();

            foreach (string key in context.Request.Form)
            {
                parameters.Add(key, context.Request.Form[key]);
            }

            foreach (string key in context.Request.QueryString)
            {
                parameters.Add(key, context.Request.QueryString[key]);
            }

            foreach (var item in _requestContext.RouteData.Values)
            {
                parameters.Add(item.Key, item.Value);
            }

            parameters.Add("HttpContext", context);
            return parameters;
        }

        private static void MapResponseToHttpResponse(Response response, HttpResponse httpResponse)
        {
            response.Headers.ForEach(header => httpResponse.AppendHeader(header.Key, header.Value));
            httpResponse.StatusCode = response.StatusCode;
            httpResponse.ContentType = response.ContentType;
            response.Contents.Invoke(httpResponse.OutputStream);
        }
    }
}
