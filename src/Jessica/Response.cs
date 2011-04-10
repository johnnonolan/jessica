using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Jessica.Responses;

namespace Jessica
{
    public class Response
    {
        static string _rootPath = AppDomain.CurrentDomain.BaseDirectory;

        public IDictionary<string, string> Headers { get; set; }
        public int StatusCode { get; set; }
        public string ContentType { get; set; }
        public Action<Stream> Contents { get; set; }

        public Response()
        {
            Headers = new Dictionary<string, string>();
            StatusCode = (int)HttpStatusCode.OK;
            ContentType = "text/html";
            Contents = GetStringContents(string.Empty);
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
