using System.Web;
using System.Web.Routing;

namespace Jessica.Routing
{
    public class NotFoundRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new NotFoundHttpHandler(requestContext);
        }
    }
}
