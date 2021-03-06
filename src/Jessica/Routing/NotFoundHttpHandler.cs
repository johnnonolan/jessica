using System.Threading;
using System.Web;
using System.Web.Routing;

namespace Jessica.Routing
{
    public class NotFoundHttpHandler : IHttpHandler
    {
        private RequestContext _requestContext;

        public NotFoundHttpHandler(RequestContext requestContext)
        {
            _requestContext = requestContext;
        }
        
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var route = _requestContext.RouteData.Values["route"] ?? string.Empty;

            if (Jess.NotFoundHandler != null)
            {
                InvokeNotFoundUserHandler(context, route.ToString());
            }
            else
            {
                InvokeNotFoundInternalHandler(context, route.ToString());
            }
        }

        private static void InvokeNotFoundInternalHandler(HttpContext context, string route)
        {
            var method = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(context.Request.HttpMethod);
            var html = @"
<!DOCTYPE html>
  <html>
    <head>
      <style type='text/css'>
        body { text-align:center;font-family:helvetica,arial;font-size:22px;color:#888;margin:20px }
        #c { margin:0 auto;width:500px;text-align:left }
      </style>
    </head>
    <body>
      <h1>Oops, Jessica doesn't know this line.</h1>
      <div id='c'>
        <p>Try this in your module:</p>
        <pre>#{method}(""/#{route}"", p => ""Hello world!"");</pre>
      </div>
    </body>
  </html>".Replace("#{method}", method).Replace("#{route}", route);

            context.Response.StatusCode = 404;
            context.Response.ContentType = "text/html";
            context.Response.Write(html);
        }

        private void InvokeNotFoundUserHandler(HttpContext context, string route)
        {
            var response = Jess.NotFoundHandler(route, _requestContext);
            context.Response.StatusCode = 404;
            context.Response.ContentType = response.ContentType;
            response.Contents(context.Response.OutputStream);
        }
    }
}