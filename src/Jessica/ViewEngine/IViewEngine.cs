using System;
using System.Collections.Generic;
using System.IO;

namespace Jessica.ViewEngine
{
    public interface IViewEngine
    {
        IEnumerable<string> Extensions { get; }

        Action<Stream> RenderView(ViewLocation viewLocation, dynamic model);
    }
}