using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Jessica.ViewEngine
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

        private static Tuple<string, string> FindViewFromShortName(string viewFolder, string viewName, IEnumerable<string> supportedExtensions)
        {
            var selectedView = from extension in supportedExtensions
                               let file = Path.Combine(viewFolder, viewName + "." + extension)
                               where File.Exists(file)
                               select Tuple.Create(file, extension);

            return selectedView.FirstOrDefault();
        }

        private ViewLocation LocateView(string viewName, IEnumerable<string> supportedExtensions)
        {
            try
            {
                var viewFolder = Path.Combine(_rootPath, Jess.Configuration.ViewsDirectory);

                if (string.IsNullOrEmpty(viewFolder))
                {
                    return null;
                }

                var selectedview = FindViewFromShortName(viewFolder, viewName, supportedExtensions);

                if (selectedview == null)
                {
                    return null;
                }

                var fileStream = new FileStream(selectedview.Item1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                return new ViewLocation(selectedview.Item1, selectedview.Item2, new StreamReader(fileStream));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}