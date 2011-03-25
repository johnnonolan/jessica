namespace Jessica.Factories
{
    public class DefaultResponseFactory : IResponseFactory
    {
        public string RootPath { get; private set; }

        public DefaultResponseFactory(string rootPath)
        {
            RootPath = rootPath;
        }
    }
}
