using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Jessica.Extensions;

namespace Jessica
{
    public class Response
    {
        public string ContentType { get; set; }

        public Action<Stream> Contents { get; set; }

        public IDictionary<string, string> Headers { get; set; }

        public int StatusCode { get; set; }

        public Response()
        {
            Contents = GetStringContents(string.Empty);
            ContentType = "text/html";
            Headers = new Dictionary<string, string>();
            StatusCode = HttpStatusCode.OK.AsInt();
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
