using System.Net;
using Jessica.Extensions;

namespace Jessica.Responses
{
    public class RedirectResponse : Response
    {
        public RedirectResponse(string location)
        {
            Headers.Add("Location", location);
            Contents = GetStringContents(string.Empty);
            ContentType = "text/html";
            StatusCode = (int)HttpStatusCode.SeeOther;
        }
    }
}
