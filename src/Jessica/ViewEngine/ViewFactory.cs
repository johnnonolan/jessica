using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Jessica.Exceptions;

namespace Jessica.ViewEngine
{
    public class ViewFactory
    {
        private ViewLocator _locator;
        private IEnumerable<IViewEngine> _viewEngines;

        public ViewFactory(IEnumerable<IViewEngine> viewEngines, string rootPath)
        {
            _viewEngines = viewEngines;
            _locator = new ViewLocator(rootPath);
        }

        public Action<Stream> RenderView(string viewName, dynamic model)
        {
            if (viewName == null && model == null)
            {
                throw new ArgumentException("viewName and model cannot both be null");
            }

            if (model == null && viewName.Length == 0)
            {
                throw new ArgumentException("viewName cannot be empty when model is null");
            }

            var actualViewName = viewName ?? GetViewNameFromModel(model);
            return GetRenderedView(actualViewName, model);
        }

        private static IEnumerable<string> GetViewExtension(string viewName)
        {
            var extension = Path.GetExtension(viewName);
            return string.IsNullOrEmpty(extension) ? null : new[] { extension.TrimStart('.') };
        }

        private static string GetViewNameFromModel(dynamic model)
        {
            return Regex.Replace(model.GetType().Name, "Model$", string.Empty);
        }

        private static Action<Stream> InvokeViewEngine(IViewEngine viewEngine, ViewLocation viewLocation, dynamic model)
        {
            try
            {
                return viewEngine.RenderView(viewLocation, model);
            }
            catch (Exception exception)
            {
                throw new RenderingViewException(exception.Message, viewLocation);
            }
        }

        private IEnumerable<string> GetExtensionsForViewLookUp(string viewName)
        {
            var extensions = GetViewExtension(viewName) ?? GetSupportedViewEngineExtensions();
            return extensions;
        }

        private Action<Stream> GetRenderedView(string viewName, dynamic model)
        {
            var viewLocation = _locator.FindView(Path.ChangeExtension(viewName, null), GetExtensionsForViewLookUp(viewName));
            var resolvedViewEngine = GetViewEngine(viewLocation);

            return InvokeViewEngine(resolvedViewEngine, viewLocation, model);
        }

        private IEnumerable<string> GetSupportedViewEngineExtensions()
        {
            var extensions = _viewEngines.SelectMany(e => e.Extensions);
            return extensions.Distinct();
        }

        private IViewEngine GetViewEngine(ViewLocation viewLocation)
        {
            if (viewLocation == null)
            {
                return null;
            }

            var viewEngines = _viewEngines.Where(engine => engine.Extensions.Any(x => x.ToUpperInvariant() == viewLocation.Extension.ToUpperInvariant()));
            return viewEngines.FirstOrDefault();
        }
    }
}