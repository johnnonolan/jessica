using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Jessica.Responses
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; }
        public Action<Stream> Contents { get; set; }
        public IDictionary<string, string> Headers { get; set; }

        public Response()
        {
            StatusCode = (int)HttpStatusCode.OK;
            ContentType = "text/html";
            Contents = GetStringContents(string.Empty);
            Headers = new Dictionary<string, string>();
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
