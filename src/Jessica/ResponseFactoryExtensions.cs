using System.IO;
using Jessica.Factories;
using Jessica.Responses;

namespace Jessica
{
    public static class ResponseFormatterExtensions
    {
        public static Response AsDownload(this IResponseFactory factory, string filePath, string contentType = "application/octet-stream")
        {
            var fullPath = Path.Combine(factory.RootPath, filePath);
            return new FileResponse(fullPath, contentType);
        }

        public static Response AsHtml(this IResponseFactory factory, string filePath)
        {
            var fullPath = Path.Combine(factory.RootPath, filePath);
            return new StaticFileResponse(fullPath);
        }

        public static Response AsCss(this IResponseFactory factory, string filePath)
        {
            var fullPath = Path.Combine(factory.RootPath, filePath);
            return new StaticFileResponse(fullPath, "text/css");
        }

        public static Response AsJs(this IResponseFactory factory, string filePath)
        {
            var fullPath = Path.Combine(factory.RootPath, filePath);
            return new StaticFileResponse(fullPath, "text/javascript");
        }

        public static Response AsJson<T>(this IResponseFactory factory, T model)
        {
            return new JsonResponse<T>(model);
        }

        public static Response AsRedirect(this IResponseFactory factory, string location)
        {
            return new RedirectResponse(location);
        }

        public static Response AsText(this IResponseFactory factory, string format, params object[] args)
        {
            return new TextResponse(format, args);
        }
    }
}
