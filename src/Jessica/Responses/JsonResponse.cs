using System;
using System.IO;
using System.Web.Script.Serialization;

namespace Jessica.Responses
{
    public class JsonResponse<T> : Response
    {
        public JsonResponse(T model)
        {
            Contents = GetJsonContents(model);
            ContentType = "application/json";
            StatusCode = 200;
        }

        private static Action<Stream> GetJsonContents(T model)
        {
            return stream =>
            {
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(model);
                var writer = new StreamWriter(stream);
                writer.Write(json);
                writer.Flush();
            };
        }
    }
}
