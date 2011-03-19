using System;
using System.IO;
using System.Net;
using Jessica.Extensions;

namespace Jessica.Responses
{
    public class StaticFileResponse : Response
    {
        public StaticFileResponse(string filePath, string contentType = "text/html")
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath) || !Path.HasExtension(filePath))
            {
                StatusCode = HttpStatusCode.NotFound.AsInt();
            }
            else
            {
                Contents = GetFileContents(filePath);
                ContentType = contentType;
                StatusCode = HttpStatusCode.OK.AsInt();
            }
        }

        private static Action<Stream> GetFileContents(string filePath)
        {
            return stream =>
            {
                using (var reader = new StreamReader(filePath))
                {
                    var writer = new StreamWriter(stream);
                    writer.Write(reader.ReadToEnd());
                    writer.Flush();
                }
            };
        }
    }
}
