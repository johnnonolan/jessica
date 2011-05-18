using System.Web;
using System.Web.Routing;
using Jessica.Extensions;

namespace Jessica.Routing
{
    public class NotFoundHttpHandler : IHttpHandler
    {
        RequestContext _requestContext;

        public NotFoundHttpHandler(RequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public void ProcessRequest(HttpContext context)
        {
            var route = _requestContext.RouteData.Values["route"] ?? "";

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
  </html>
";

            html = html.Replace("#{method}", context.Request.HttpMethod.ToTitleCase());
            html = html.Replace("#{route}", route.ToString());

            context.Response.StatusCode = 404;
            context.Response.ContentType = "text/html";
            context.Response.Write(html);
            context.Response.End();
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
