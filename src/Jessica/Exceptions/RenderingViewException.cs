using System;
using Jessica.ViewEngine;

namespace Jessica.Exceptions
{
    public class RenderingViewException : Exception
    {
        public RenderingViewException(string message, ViewLocation viewLocation)
            : base(message)
        {
            ViewLocation = viewLocation;
        }

        public ViewLocation ViewLocation { get; set; }
    }
}