using System;
using System.Collections.Generic;
using System.IO;
using Jessica.ViewEngine;

namespace Jessica.Tests.Fakes.ViewEngine
{
    public class FakeViewEngine : IViewEngine
    {
        public IEnumerable<string> Extensions
        {
            get { return new[] { "html" }; }
        }

        public Action<Stream> RenderView(ViewLocation viewLocation, dynamic model)
        {
            if (viewLocation == null)
            {
                return stream => { };
            }

            return stream =>
            {
                var writer = new StreamWriter(stream);
                writer.Write(viewLocation.Contents.ReadToEnd());
                writer.Flush();
            };
        }
    }
}
