using System;
using System.Collections.Generic;
using Jessica.ViewEngines;

namespace Jessica.Tests.Fakes.ViewEngines
{
    public class AnotherViewEngine : IViewEngine
    {
        public IEnumerable<string> Extensions
        {
            get { return new[] { "another" }; }
        }

        public Action<System.IO.Stream> RenderView(ViewLocation viewLocation, dynamic model)
        {
            throw new NotImplementedException();
        }

        public AnotherViewEngine()
        {
        }

        public AnotherViewEngine(object obj)
        {
        }
    }
}
