using System;
using System.IO;
using System.Net;

namespace Jessica.Responses
{
    public class FileResponse : Response
    {
        public FileResponse(string filePath, string contentType = "application/octet-stream")
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath) || !Path.HasExtension(filePath))
            {
                StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                Contents = GetFileContents(filePath);
                ContentType = contentType;
                StatusCode = (int)HttpStatusCode.OK;
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
