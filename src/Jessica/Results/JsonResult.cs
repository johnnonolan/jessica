using System.Web;
using System.Web.Script.Serialization;

namespace Jessica.Results
{
    public class JsonResult : IActionResult
    {
        private readonly object _data;

        public JsonResult(object data)
        {
            _data = data;
        }

        public void WriteToResponse(HttpContextBase context)
        {
            context.Response.ContentType = "text/json";

            if (_data != null)
            {
                var json = new JavaScriptSerializer().Serialize(_data);
                context.Response.Write(json);
            }
            else
            {
                context.Response.Write(string.Empty);
            }
        }
    }
}
