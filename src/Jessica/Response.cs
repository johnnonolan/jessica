using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Jessica.Extensions;

namespace Jessica
{
    public class Response
    {
        public IDictionary<string, string> Headers { get; set; }
        public int StatusCode { get; set; }
        public string ContentType { get; set; }
        public Action<Stream> Contents { get; set; }

        public Response()
        {
            Headers = new Dictionary<string, string>();
            StatusCode = HttpStatusCode.OK.AsInt();
            ContentType = "text/html";
            Contents = GetStringContents(string.Empty);            
        }

        public static implicit  operator Response(Action<Stream> action)
        {
            return new Response { Contents = action };
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
