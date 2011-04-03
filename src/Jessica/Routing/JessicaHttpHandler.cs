﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Routing;
using Jessica.Extensions;

namespace Jessica.Routing
{
    public class JessicaHttpHandler : IHttpHandler
    {
        private string _route;
        private RequestContext _requestContext;
        private Type _moduleType;

        public JessicaHttpHandler(string route, RequestContext requestContext, Type moduleType)
        {
            _route = route;
            _requestContext = requestContext;
            _moduleType = moduleType;
        }

        public bool IsReusable 
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var module = Jess.Factory.CreateInstance(_moduleType) as JessModule;

            if (module != null)
            {
                var route = module.Routes.Single(r => r.Url == _route);
                var method = context.Request.HttpMethod.ToUpper();

                if (route.Actions[method] != null)
                {
                    IDictionary<string, object> parameters = new ExpandoObject();

                    AddQueryStringParameters(parameters, context.Request);
                    AddFormParameters(parameters, context.Request);
                    AddRouteParameters(parameters, _requestContext.RouteData);

                    parameters.Add("HttpContext", context);

                    module.Before.Invoke(_requestContext);
                    var response = route.Actions[method].Invoke(parameters);
                    module.After.Invoke(_requestContext);

                    MapResponseToHttpResponse(response, context.Response);

                    response.Contents.Invoke(context.Response.OutputStream);
                }
                else
                {
                    context.Response.StatusCode = HttpStatusCode.MethodNotAllowed.AsInt();
                }
            }
            else
            {
                context.Response.StatusCode = HttpStatusCode.InternalServerError.AsInt();
            }
        }

        private static void AddFormParameters(IDictionary<string, object> parameters, HttpRequest request)
        {
            foreach (string key in request.Form)
            {
                parameters.Add(key, request.Form[key]);
            }
        }

        private static void AddQueryStringParameters(IDictionary<string, object> parameters, HttpRequest request)
        {
            foreach (string key in request.QueryString)
            {
                parameters.Add(key, request.QueryString[key]);
            }
        }

        private static void AddRouteParameters(IDictionary<string, object> parameters, RouteData routeData)
        {
            if (routeData != null)
            {
                routeData.Values.ForEach(p => parameters.Add(p.Key, p.Value));
            }
        }

        private static void MapResponseToHttpResponse(Response response, HttpResponse httpResponse)
        {
            response.Headers.ForEach(header => httpResponse.AddHeader(header.Key, header.Value));
            httpResponse.StatusCode = response.StatusCode;
            httpResponse.ContentType = response.ContentType;
        }
    }
}
