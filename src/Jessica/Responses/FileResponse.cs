using System;
using System.IO;
using System.Net;
using Jessica.Extensions;

namespace Jessica.Responses
{
    public class FileResponse : Response
    {
        public FileResponse(string filePath, string contentType = "application/octet-stream")
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
                var contents = File.ReadAllBytes(filePath);
                stream.Write(contents, 0, contents.Length);
            };
        }
    }
}
