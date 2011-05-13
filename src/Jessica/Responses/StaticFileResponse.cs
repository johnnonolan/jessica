using System;
using System.IO;

namespace Jessica.Responses
{
    public class StaticFileResponse : Response
    {
        public StaticFileResponse(string filePath, string contentType = "text/html")
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath) || !Path.HasExtension(filePath))
            {
                StatusCode = 404;
            }
            else
            {
                Contents = GetFileContents(filePath);
                ContentType = contentType;
                StatusCode = 200;
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
