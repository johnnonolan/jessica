using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Jessica.ViewEngines
{
    public class ViewLocator
    {
        private string _rootPath;

        public ViewLocator(string rootPath)
        {
            _rootPath = rootPath;
        }

        public ViewLocation FindView(string viewName, IEnumerable<string> supportedExtensions)
        {
            if (supportedExtensions == null)
            {
                return null;
            }

            supportedExtensions = supportedExtensions.ToList();

            if (string.IsNullOrEmpty(viewName) || !supportedExtensions.Any())
            {
                return null;
            }

            return LocateView(viewName, supportedExtensions);
        }

        private ViewLocation LocateView(string viewName, IEnumerable<string> supportedExtensions)
        {
            try
            {
                var viewFolder = Path.Combine(_rootPath, "Views");

                if (string.IsNullOrEmpty(viewFolder))
                {
                    return null;
                }

                var selectedView = FindViewFromName(viewFolder, viewName, supportedExtensions);
                var fileStream = new FileStream(selectedView.Item1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                return new ViewLocation(selectedView.Item1, selectedView.Item2, new StreamReader(fileStream));
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Tuple<string, string> FindViewFromName(string viewFolder, string viewName, IEnumerable<string> supportedExtensions)
        {
            var selectedView = from extension in supportedExtensions
                               let file = Path.Combine(viewFolder, viewName + "." + extension)
                               where File.Exists(file)
                               select Tuple.Create(file, extension);

            return selectedView.FirstOrDefault();
        }
    }
}
