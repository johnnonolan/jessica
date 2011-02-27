using System;
using System.IO;
using System.Web;
using Jessica.Extensions;

namespace Jessica.Results
{
    public class HtmlResult : IActionResult
    {
        private readonly string _path;

        public HtmlResult(string path)
        {
            _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

            if (!File.Exists(_path))
            {
                throw new FileNotFoundException("The file '{0}' could not be found".With(_path));
            }
        }

        public void WriteToResponse(HttpContextBase context)
        {
            var content = File.ReadAllText(_path);

            context.Response.ContentType = "text/html";
            context.Response.Write(content);
        }
    }
}
