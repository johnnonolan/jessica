using System.Web;

namespace Jessica.Results
{
    public class StringResult : IActionResult
    {
        private readonly string _data;

        public StringResult(string data)
        {
            _data = data;
        }

        public void WriteToResponse(HttpContextBase context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(_data);
        }
    }
}
