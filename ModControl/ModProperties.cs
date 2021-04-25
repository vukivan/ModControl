using System.Xml;

namespace ModControl
{
    internal class ModProperties
    {
        public string author;
        public string title;
        public string version;
        public string icon;
        public string filename;

        public ModProperties(string filename, string title, string author, string version, string icon)
        {
            this.title = title;
            this.author = author;
            this.version = version;
            this.icon = icon;
            this.filename = filename;
        }
    }
}