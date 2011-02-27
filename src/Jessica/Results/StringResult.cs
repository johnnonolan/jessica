using System.Web;
using Jessica.Extensions;

namespace Jessica.Results
{
    public class StringResult : IActionResult
    {
        private readonly string _data;

        public StringResult(string data)
        {
            _data = data;
        }

        public StringResult(string format, params object[] args)
        {
            _data = format.With(args);
        }

        public void WriteToResponse(HttpContextBase context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(_data);
        }
    }
}
