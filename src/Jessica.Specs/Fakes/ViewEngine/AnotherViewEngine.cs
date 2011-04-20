using System;
using System.Collections.Generic;
using System.IO;
using Jessica.ViewEngine;

namespace Jessica.Specs.Fakes.ViewEngine
{
    public class AnotherViewEngine : IViewEngine
    {
        public IEnumerable<string> Extensions
        {
            get { throw new NotImplementedException(); }
        }

        public Action<Stream> RenderView(ViewLocation viewLocation, dynamic model)
        {
            throw new NotImplementedException();
        }
    }
}
