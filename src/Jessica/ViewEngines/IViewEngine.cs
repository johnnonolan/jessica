using System;
using System.Collections.Generic;
using System.IO;

namespace Jessica.ViewEngines
{
    public interface IViewEngine
    {
        IEnumerable<string> Extensions { get; }

        Action<Stream> RenderView(ViewLocation viewLocation, dynamic model);
    }
}
