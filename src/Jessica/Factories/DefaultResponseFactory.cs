namespace Jessica.Factories
{
    public class DefaultResponseFactory : IResponseFactory
    {
        private string _rootPath;

        public string RootPath
        {
            get { return _rootPath; }
        }

        public DefaultResponseFactory(string rootPath)
        {
            _rootPath = rootPath;
        }
    }
}