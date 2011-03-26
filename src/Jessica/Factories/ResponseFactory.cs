namespace Jessica.Factories
{
    public class ResponseFactory
    {
        public string RootPath { get; private set; }

        public ResponseFactory(string rootPath)
        {
            RootPath = rootPath;
        }
    }
}
