using System;
using System.IO;
using System.Xml.Serialization;

namespace Jessica.Responses
{
    public class XmlResponse<T> : Response
    {
        public XmlResponse(T model)
        {
            Contents = GetXmlContents(model);
            ContentType = "application/xml";
            StatusCode = 200;
        }

        private static Action<Stream> GetXmlContents(T model)
        {
            return stream =>
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, model);
            };
        }
    }
}