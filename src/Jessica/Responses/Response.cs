using System;
using System.Collections.Generic;
using System.IO;

namespace Jessica.Responses
{
    public class Response
    {
        public Response()
        {
            StatusCode = 200;
            ContentType = "text/html";
            Contents = GetStringContents(string.Empty);
            Headers = new Dictionary<string, string>();
        }

        public Action<Stream> Contents { get; set; }

        public string ContentType { get; set; }

        public IDictionary<string, string> Headers { get; set; }

        public int StatusCode { get; set; }

        public static Response AsCss(string filePath)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Jess.Configuration.PublicDirectory, filePath);
            return new StaticFileResponse(path, "text/css");
        }

        public static Response AsFile(string filePath, string contentType = "application/octet-stream")
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Jess.Configuration.PublicDirectory, filePath);
            return new FileResponse(path, contentType);
        }

        public static Response AsHtml(string filePath)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Jess.Configuration.PublicDirectory, filePath);
            return new StaticFileResponse(path);
        }

        public static Response AsJs(string filePath)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Jess.Configuration.PublicDirectory, filePath);
            return new StaticFileResponse(path, "text/javascript");
        }

        public static Response AsJson<T>(T model)
        {
            return new JsonResponse<T>(model);
        }

        public static Response AsRedirect(string location)
        {
            return new RedirectResponse(location);
        }

        public static Response AsText(string format, params object[] args)
        {
            return new TextResponse(format, args);
        }

        public static implicit operator Response(Action<Stream> action)
        {
            return new Response { Contents = action };
        }

        public static implicit operator Response(int statusCode)
        {
            return new Response { StatusCode = statusCode };
        }

        public static implicit operator Response(string contents)
        {
            return new Response { Contents = GetStringContents(contents) };
        }

        protected static Action<Stream> GetStringContents(string contents)
        {
            return stream =>
            {
                var writer = new StreamWriter(stream) { AutoFlush = true };
                writer.Write(contents);
            };
        }
    }
}