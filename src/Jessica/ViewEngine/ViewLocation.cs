using System.IO;

namespace Jessica.ViewEngine
{
    public class ViewLocation
    {
        public string Location { get; private set; }
        public string Extension { get; private set; }
        public TextReader Contents { get; private set; }

        public ViewLocation(string location, string extension, TextReader contents)
        {
            Location = location;
            Extension = extension;
            Contents = contents;
        }
    }
}
