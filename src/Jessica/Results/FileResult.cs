using System;
using System.IO;
using System.Web;
using Jessica.Extensions;
using Microsoft.Win32;

namespace Jessica.Results
{
    public class FileResult : IActionResult
    {
        private readonly string _path;

        public FileResult(string path)
        {
            _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

            if (!File.Exists(_path))
            {
                throw new FileNotFoundException("The file '{0}' could not be found".With(_path));
            }
        }

        public void WriteToResponse(HttpContextBase context)
        {
            var contentType = "application/unknown";
            var extension = Path.GetExtension(_path);

            if (extension != null)
            {
                var key = Registry.ClassesRoot.OpenSubKey(extension.ToLower());

                if (key != null && key.GetValue("Content Type") != null)
                {
                    contentType = key.GetValue("Content Type").ToString();
                }
            }

            context.Response.ContentType = contentType;
            context.Response.AddHeader("Content-Disposition", "attachment; filename={0}".With(Path.GetFileName(_path)));
            context.Response.WriteFile(_path);
        }
    }
}
