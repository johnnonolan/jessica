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

                var filesInViewFolder = Directory.GetFiles(viewFolder);
                var viewFiles = filesInViewFolder
                    .SelectMany(file => supportedExtensions, (file, extension) => new { file, extension })
                    .Where(o => Path.GetFileName(o.file.ToUpperInvariant()) == string.Concat(viewName, ".", o.extension).ToUpperInvariant())
                    .Select(o => new { o.file, o.extension });

                var selectedView = viewFiles.FirstOrDefault();
                var fileStream = new FileStream(selectedView.file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                return new ViewLocation(selectedView.file, selectedView.extension, new StreamReader(fileStream));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
