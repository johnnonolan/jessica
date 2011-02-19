using System.Web;

namespace Jessica.Results
{
    public class RedirectResult : IActionResult
    {
        private readonly string _url;

        public RedirectResult(string url)
        {
            _url = url;
        }

        public void WriteToResponse(HttpContextBase context)
        {
            context.Response.Redirect(_url, true);
        }
    }
}
