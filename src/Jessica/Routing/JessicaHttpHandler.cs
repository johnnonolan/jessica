using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Routing;
using Jessica.Extensions;
using Jessica.Filters;
using Jessica.Responses;

namespace Jessica.Routing
{
    public class JessicaHttpHandler : IHttpHandler
    {
        string _route;
        RequestContext _requestContext;
        Type _moduleType;

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
            var response = InvokeRequestLifeCycle(context);
            MapResponseToHttpResponse(response, context.Response);
        }

        private Response InvokeRequestLifeCycle(HttpContext context)
        {
            var module = Jess.Factory.CreateInstance(_moduleType) as JessModule;

            if (module == null)
            {
                return (int)HttpStatusCode.InternalServerError;
            }

            var route = module.Routes.Single(r => r.Url == _route);
            var method = context.Request.HttpMethod.ToUpper();

            if (!route.Actions.ContainsKey(method))
            {
                return (int)HttpStatusCode.MethodNotAllowed;
            }

            var response = InvokeBeforeFilters(module);

            if (response == null)
            {
                response = route.Actions[method].Invoke(BuildParameterObject(context));
            }

            if (module.After != null)
            {
                module.After.Invoke(_requestContext);
            }

            return response;
        }

        private Response InvokeBeforeFilters(JessModule module)
        {
            if (module.Before != null)
            {
                return module.Before.Invoke(_requestContext);
            }

            return null;
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
